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
            Reading.SetupDataTable();
        }

        private void loadB_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Open File";
            openFileDialog.Filter = "Parsed Sniff File (*.txt)|*.txt";
            openFileDialog.FileName = "*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel.Text = "Loading...";
                statusStrip.Update();
                Data.fileName = System.IO.Path.GetFileName(openFileDialog.FileName);
                LoadSniffFileIntoDatatable(openFileDialog.FileName);
                toolStripStatusLabel.Text = Data.fileName;
            }
            else
            {
                // This code runs if the dialog was cancelled
                return;
            }
        }

        public void LoadSniffFileIntoDatatable(string fileName)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            var line = file.ReadLine();
            file.Close();

            if (line == "# TrinityCore - WowPacketParser")
            {
                Utility.WriteLog("- Exporting creature data from parsed sniff..");
                Reading.GetCreatureSpells(fileName.ToString());
                Data.ParseData();
                FillListBoxs();
            }
            else
            {
                MessageBox.Show(openFileDialog.FileName + " is is not a valid TrinityCore parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void FillListBoxs()
        {
            List<string> lst = new List<string>();

            foreach (DataRow r in objectDataTable.Rows)
            {
                string objectID = r["ObjectID"].ToString();

                if (!lst.Contains(objectID))
                    lst.Add(r["ObjectID"].ToString());
            }

            lst.Sort();
            if (entryListBox.DataSource != lst)
                entryListBox.DataSource = lst;
            entryListBox.Refresh();
        }

        private void entryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lst = new List<string>();
            string ObjectID = entryListBox.SelectedItem.ToString();

            DataRow[] objectIDResult = Frm_ReadInfo.objectDataTable.Select("ObjectID = '" + ObjectID + "'");
            foreach (DataRow r in objectIDResult)
            {
                string spellID = r["SpellID"].ToString();
                string CasterTargetID = r["CasterTargetID"].ToString();
                if (!lst.Contains(spellID))
                {
                    if (CasterTargetID != "")
                        lst.Add(spellID + " " + CasterTargetID);
                    else
                        lst.Add(spellID);
                }
            }

            lst.Sort();
            if (spellListBox.DataSource != lst)
                spellListBox.DataSource = lst;
            spellListBox.Refresh();
        }

        private void spellListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedInfo = spellListBox.SelectedItem.ToString();
            spellRichTextBox.Text = "";

            if (selectedInfo.Contains(" "))
            {
                string[] selectedInfo_split;
                selectedInfo_split = Regex.Split(selectedInfo, " ");

                if (selectedInfo_split.Length > 0)
                {
                    DataRow[] spellIDResult = Frm_ReadInfo.objectDataTable.Select("SpellID = '" + selectedInfo_split[0] + "' AND CasterTargetID = '" + selectedInfo_split[1] + "'");
                    foreach (DataRow r in spellIDResult)
                    {
                        string objectid = r["ObjectID"].ToString();
                        string objecttype = r["ObjectType"].ToString();
                        string spellid = r["SpellID"].ToString();
                        string castflags = r["CastFlags"].ToString();
                        string castflagsex = r["CastFlagsEx"].ToString();
                        string castertargetid = r["CasterTargetID"].ToString();
                        string castertarget = r["CasterTarget"].ToString();
                        string intervaltime = r["IntervalTime"].ToString();


                        StringBuilder sb = new StringBuilder(spellRichTextBox.Text);
                        //sb.Append(Environment.NewLine);
                        sb.Append("ObjectID: " + objectid + Environment.NewLine);
                        sb.Append("ObjectType: " + objecttype + Environment.NewLine);
                        sb.Append("SpellID: " + spellid + Environment.NewLine);
                        sb.Append("CastFlags: " + castflags + Environment.NewLine);
                        sb.Append("CastFlagsEx: " + castflagsex + Environment.NewLine);
                        sb.Append("CasterTargetID: " + castertargetid + Environment.NewLine);
                        sb.Append("CasterTarget: " + castertarget + Environment.NewLine);
                        sb.Append("IntervalTime: " + intervaltime + Environment.NewLine);

                        spellRichTextBox.Text = sb.ToString();
                        return;
                    }
                }
            }
            else
            {
                DataRow[] spellIDResult = Frm_ReadInfo.objectDataTable.Select("SpellID = '" + selectedInfo + "'");
                foreach (DataRow r in spellIDResult)
                {
                    string objectid = r["ObjectID"].ToString();
                    string objecttype = r["ObjectType"].ToString();
                    string spellid = r["SpellID"].ToString();
                    string castflags = r["CastFlags"].ToString();
                    string castflagsex = r["CastFlagsEx"].ToString();
                    string castertargetid = r["CasterTargetID"].ToString();
                    string castertarget = r["CasterTarget"].ToString();
                    string intervaltime = r["IntervalTime"].ToString();

                    StringBuilder sb = new StringBuilder(spellRichTextBox.Text);
                    sb.Append("ObjectID: " + objectid + Environment.NewLine);
                    sb.Append("ObjectType: " + objecttype + Environment.NewLine);
                    sb.Append("SpellID: " + spellid + Environment.NewLine);
                    sb.Append("CastFlags: " + castflags + Environment.NewLine);
                    sb.Append("CastFlagsEx: " + castflagsex + Environment.NewLine);
                    sb.Append("CasterTargetID: " + castertargetid + Environment.NewLine);
                    sb.Append("CasterTarget: " + castertarget + Environment.NewLine);
                    sb.Append("IntervalTime: " + intervaltime + Environment.NewLine);

                    spellRichTextBox.Text = sb.ToString();
                    return;
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
    }
}
