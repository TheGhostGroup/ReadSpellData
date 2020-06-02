using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ReadSpellData
{
    class Utility
    {
        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string[] Path = new string[] { AppDomain.CurrentDomain.BaseDirectory, "\\LOG\\" };
            string logFilePath = string.Concat(Path);

            logFilePath = logFilePath + "Log_" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";

            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            string timestr = "[" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "]  ";
            log.WriteLine(timestr + strLog);
            log.Close();
        }

        // Shows a dialog with the query.
        public static DialogResult ShowSaveDialog(ref string query)
        {
            System.Drawing.Size size = new System.Drawing.Size(800, 450);
            Form saveBox = new Form();

            saveBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            saveBox.ClientSize = size;
            saveBox.Text = "Save Data";
            saveBox.MaximizeBox = false;
            saveBox.MinimizeBox = false;
            saveBox.StartPosition = FormStartPosition.CenterParent;

            System.Windows.Forms.RichTextBox textBox = new RichTextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, size.Height - 40);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = query;
            textBox.Multiline = true;
            textBox.WordWrap = false;
            textBox.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            saveBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "Save";
            okButton.Location = new System.Drawing.Point(size.Width / 2 - okButton.Size.Width - 2, textBox.Location.Y + textBox.Size.Height + 5);
            saveBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "Cancel";
            cancelButton.Location = new System.Drawing.Point(okButton.Location.X + okButton.Size.Width + 4, textBox.Location.Y + textBox.Size.Height + 5);
            saveBox.Controls.Add(cancelButton);

            saveBox.CancelButton = cancelButton;

            DialogResult result = saveBox.ShowDialog();
            query = textBox.Text;
            return result;
        }
        // Shows an input box that returns a value.
        public static DialogResult ShowInputDialog(ref string input, string name)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = name;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.StartPosition = FormStartPosition.CenterParent;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }
    }
    public class MixedListSorter : System.Collections.IComparer
    {
        public int Column = 0;
        public System.Windows.Forms.SortOrder Order = SortOrder.Ascending;
        public int Compare(object x, object y) // IComparer Member
        {
            if (!(x is ListViewItem))
                return (0);
            if (!(y is ListViewItem))
                return (0);

            ListViewItem l1 = (ListViewItem)x;
            ListViewItem l2 = (ListViewItem)y;

            int intValue1;

            if (Int32.TryParse(l1.SubItems[Column].Text, out intValue1))
            {
                int intValue2;
                Int32.TryParse(l2.SubItems[Column].Text, out intValue2);

                if (Order == SortOrder.Ascending)
                {
                    return intValue1.CompareTo(intValue2);
                }
                else
                {
                    return intValue2.CompareTo(intValue1);
                }
            }
            else
            {
                string str1 = l1.SubItems[Column].Text;
                string str2 = l2.SubItems[Column].Text;

                if (Order == SortOrder.Ascending)
                {
                    return str1.CompareTo(str2);
                }
                else
                {
                    return str2.CompareTo(str1);
                }
            }
        }
    }
}
