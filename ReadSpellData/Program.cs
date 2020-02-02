using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;

namespace ReadSpellData
{
    class Program
    {
        public static DataTable objectDataTable = new DataTable("Objects");
        public static DataTable objectTimingDataTable = new DataTable("ObjectTiming");
        public static string fileName = ConfigurationManager.AppSettings["file"].ToString();

        static void Main(string[] args)
        {
            Console.WriteLine("- Exporting creature data from parsed sniff..");
            string colObject = "ObjectID,ObjectType,SpellID,CastFlags,CastFlagsEx,CasterTargetID,CasterTarget,Time,IntervalTime,Number";
            // Setup Columns for for objectDataTable
            string[] columns = colObject.Split(new char[] { ',' });
            foreach (var column in columns)
                Program.objectDataTable.Columns.Add(column);

            string colObjectTiming = "ObjectID";
            // Setup Columns for for objectTimingDataTable
            string[] columnsTiming = colObjectTiming.Split(new char[] { ',' });
            foreach (var column in columnsTiming)
                Program.objectTimingDataTable.Columns.Add(column);

            Reading.GetCreatureSpells();
            Data.Checking();
            Export.ExportXLS();
        }
    }
}
