using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
    }
}
