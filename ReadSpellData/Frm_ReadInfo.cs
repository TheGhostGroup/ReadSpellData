using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.Text.RegularExpressions; // Regex

namespace ReadSpellData
{
    public partial class Frm_ReadInfo : Form
    {
        public static DataTable objectDataTable = new DataTable("Objects");
        public static DataTable objectTimingDataTable = new DataTable("ObjectTiming");
        public static List<string> entryList = new List<string>();

        public Frm_ReadInfo()
        {
            InitializeComponent();
            lstSpellCasts.ListViewItemSorter = new MixedListSorter();
            Reading.SetupDataTable();
        }

        private void loadB_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Open File";
            openFileDialog.Filter = "Parsed Sniff File (*.txt)|*.txt";
            openFileDialog.FileName = "*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.Multiselect = true;
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileNames.Length == 1)
                    LoadParsedSniff(openFileDialog.FileName);
                else
                {
                    string sql = "";
                    foreach (String fileName in openFileDialog.FileNames)
                    {
                        LoadParsedSniff(fileName);
                        sql += Data.GenerateSaveString();
                        
                    }
                    if (Utility.ShowSaveDialog(ref sql) == DialogResult.OK)
                        Database.WriteDB(sql);
                }
                
            }
            else
            {
                // This code runs if the dialog was cancelled
                return;
            }
        }

        public void LoadParsedSniff(string fileName)
        {
            Data.clientBuild = Reading.GetBuild(fileName);
            if (Data.clientBuild == 0)
            {
                string buildString = "";
                if (Utility.ShowInputDialog(ref buildString, "Enter client build number") == DialogResult.OK)
                {
                    UInt32.TryParse(buildString, out Data.clientBuild);
                }
            }
            toolStripStatusLabel.Text = "Loading...";
            statusStrip.Update();
            Data.fileName = System.IO.Path.GetFileName(fileName);
            LoadSniffFileIntoDatatable(fileName);
            toolStripStatusLabel.Text = Data.fileName;
        }

        public void LoadSniffFileIntoDatatable(string fileName)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            var line = file.ReadLine();
            file.Close();

            if (line == "# TrinityCore - WowPacketParser")
            {
                Utility.WriteLog("- Exporting creature data from parsed sniff..");

                if (Data.clientBuild <= 12340)
                    Reading.GetCreatureSpellsWotlk(fileName.ToString());
                else
                    Reading.GetCreatureSpellsClassic(fileName.ToString());

                Data.ParseData();
                FillListView();
            }
            else
            {
                MessageBox.Show(fileName + " is is not a valid TrinityCore parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void FillListView()
        {
            lstSpellCasts.Items.Clear();
            if (chkShowUnique.Checked)
            {
                foreach (SpellCastData castDetails in Data.castsList)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = castDetails.casterId.ToString(); ;
                    lvi.SubItems.Add(castDetails.casterType);
                    lvi.SubItems.Add(castDetails.spellId.ToString());
                    lvi.SubItems.Add(castDetails.castFlags.ToString());
                    lvi.SubItems.Add(castDetails.castFlagsEx.ToString());
                    lvi.SubItems.Add(castDetails.targetId.ToString());
                    lvi.SubItems.Add(castDetails.targetType);

                    string cooldown = "";
                    SpellCooldownKey cdKey = new SpellCooldownKey(castDetails.casterId, castDetails.casterType, castDetails.spellId);
                    if (Data.spellCooldowns.ContainsKey(cdKey))
                        cooldown = Data.spellCooldowns[cdKey].cooldownMin.ToString() + " - " + Data.spellCooldowns[cdKey].cooldownMax.ToString();
                    lvi.SubItems.Add(cooldown);

                    lstSpellCasts.Items.Add(lvi);
                }
            }
            else
            {
                foreach (DataRow rowDetails in Frm_ReadInfo.objectDataTable.Rows)
                {
                    string rowObjectID = rowDetails["ObjectID"].ToString();
                    string rowObjectType = rowDetails["ObjectType"].ToString();
                    string rowSpellID = rowDetails["SpellID"].ToString();
                    string rowCastFlags = rowDetails["CastFlags"].ToString();
                    string rowCastFlagsEx = rowDetails["CastFlagsEx"].ToString();
                    string rowTargetId = rowDetails["CasterTarget"].ToString();
                    string rowTargetType = rowDetails["CasterTargetID"].ToString();
                    string rowTime = rowDetails["Time"].ToString();

                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = rowObjectID;
                    lvi.SubItems.Add(rowObjectType);
                    lvi.SubItems.Add(rowSpellID);
                    lvi.SubItems.Add(rowCastFlags);
                    lvi.SubItems.Add(rowCastFlagsEx);
                    lvi.SubItems.Add(rowTargetId);
                    lvi.SubItems.Add(rowTargetType);
                    lvi.SubItems.Add(rowTime);
                    lstSpellCasts.Items.Add(lvi);
                }
            }
        }

        private void exportB_Click(object sender, EventArgs e)
        {
            Export.ExportXLS();
        }

        private void exportSQLB_Click(object sender, EventArgs e)
        {
            string sql = Data.GenerateSaveString();
            if (Utility.ShowSaveDialog(ref sql) == DialogResult.OK)
                Database.WriteDB(sql);
        }

        private void lstSpellCasts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lstSpellCasts.ListViewItemSorter == null)
                return;

            // Sort the items when column is clicked.
            MixedListSorter s = (MixedListSorter)lstSpellCasts.ListViewItemSorter;
            s.Column = e.Column;

            if (s.Order == System.Windows.Forms.SortOrder.Ascending)
                s.Order = System.Windows.Forms.SortOrder.Descending;
            else
                s.Order = System.Windows.Forms.SortOrder.Ascending;
            lstSpellCasts.Sort();
        }

        private void chkShowUnique_CheckedChanged(object sender, EventArgs e)
        {
            // Change name of last column since showing Time for filtered list is not useful
            if (chkShowUnique.Checked)
                lstSpellCasts.Columns[7].Text = "Cooldown";
            else
                lstSpellCasts.Columns[7].Text = "Time";

            FillListView();
        }

        private void Frm_ReadInfo_ResizeEnd(object sender, EventArgs e)
        {
            lstSpellCasts.Size = new Size(this.Size.Width - 32, statusStrip.Location.Y - lstSpellCasts.Location.Y - 10);
        }
    }
}
