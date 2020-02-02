using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions; // Regex

namespace ReadSpellData
{
    class Data
    {
        public static List<int> deleteList = new List<int>();
        //List<DataRow> deleteList = new List<DataRow>();
        public static void Checking()
        {
            // min/max timing check
            foreach (DataRow rowData in Program.objectDataTable.Rows)
            {
                int ObjectID = Convert.ToInt32(rowData[0]);

                // Insert into object timing table
                DataRow dr = Program.objectTimingDataTable.NewRow();
                dr[0] = ObjectID;
                Program.objectTimingDataTable.Rows.Add(dr);

                DataRow[] objectTimingResult = Program.objectTimingDataTable.Select("ObjectID = '" + ObjectID + "'");
                if (objectTimingResult.Length <= 2)
                {
                    DataRow[] objectResult = Program.objectDataTable.Select("ObjectID = '" + ObjectID + "'");
                    if (objectResult.Length > 1)
                    {
                        Console.WriteLine("Checking ObjectID: " + ObjectID);
                        DateTime? dateTimeFirst = null;
                        DateTime? dateTimeSecond = null;
                        int index = Program.objectDataTable.Rows.IndexOf(rowData);

                        foreach (DataRow rowDetails in objectResult)
                        {
                            if (!dateTimeFirst.HasValue)
                                dateTimeFirst = Convert.ToDateTime(rowDetails[7]);

                            if (dateTimeFirst.HasValue)
                                dateTimeSecond = Convert.ToDateTime(rowDetails[7]);
                        }

                        TimeSpan difference = TimeSpan.Zero;
                        TimeSpan duration = dateTimeFirst.Value - dateTimeSecond.Value;
                        difference = difference.Add(duration);
                        Console.WriteLine("Found difference in time: " + difference);
                        Program.objectDataTable.Rows[index][8] = difference.ToString();
                    }
                }
            }

            // delete unwanted records
            foreach (DataRow rowDetails in Program.objectDataTable.Rows)
            {
                bool delete = false;
                string ObjectType = rowDetails["ObjectType"].ToString();
                string IntervalTime = rowDetails["IntervalTime"].ToString();
                int Number = Convert.ToInt32(rowDetails["Number"].ToString());

                if (ObjectType == "Pet/0")
                    delete = true;

                if (ObjectType == "Item/0")
                    delete = true;

                if (IntervalTime == "")
                    delete = true;

                if (delete == true)
                    deleteList.Add(Number);
            }

            foreach (var deleteItemNumber in deleteList)
            {
                Console.WriteLine("Delete: " + deleteItemNumber);
                try
                {
                    DataRow[] row = Program.objectDataTable.Select("Number = '" + deleteItemNumber + "'");
                    for (int i = row.Length - 1; i >= 0; i--)
                    {
                        Console.WriteLine("Delete: " + deleteItemNumber);
                        row[i].Delete();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
