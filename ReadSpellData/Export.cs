using System;
using System.Linq;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ReadSpellData
{
    class Export
    {
        public static void ExportXLS()
        {
            Console.WriteLine("- Exporting CreatureData to Excel document...");
            using (var workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(Program.objectDataTable, "ObjectData");

                // Save
                workbook.SaveAs("Data.xlsx");
            }
        }
    }
}
