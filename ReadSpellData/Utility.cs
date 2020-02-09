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
        public static void WriteToRichTextBoxSpellInfo(string text)
        {
            try
            {
                Frm_ReadInfo form = new Frm_ReadInfo();
                StringBuilder sb = new StringBuilder(form.spellRichTextBox.Text);
                sb.Append(Environment.NewLine);
                sb.Append(text);
                form.spellRichTextBox.Text = sb.ToString();
            }
            catch (Exception e)
            {
                WriteLog(e.ToString());
            }
        }

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
    }
}
