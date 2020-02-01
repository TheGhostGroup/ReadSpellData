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
        public static DataTable creatureDataTable = new DataTable("Creatures");
        public static string fileName = ConfigurationManager.AppSettings["file"].ToString();

        static void Main(string[] args)
        {
            Console.WriteLine("- Exporting creature data from parsed sniff..");
            string colCreature = "ObjectID,ObjectType,SpellID,CastFlags,CastFlagsEx,CasterTargetID,CasterTarget,Time";
            // Setup Columns for for creatureDataTable
            string[] columns = colCreature.Split(new char[] { ',' });
            foreach (var column in columns)
                Program.creatureDataTable.Columns.Add(column);

            Reading.GetCreatureSpells();
            Export.ExportXLS();
        }
    }
}
