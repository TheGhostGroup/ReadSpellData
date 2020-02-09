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
        public static void Checking()
        {
            // min/max timing check
            foreach (DataRow rowData in Frm_ReadInfo.objectDataTable.Rows)
            {
                int ObjectID = Convert.ToInt32(rowData[0]);

                // Insert into object timing table
                DataRow dr = Frm_ReadInfo.objectTimingDataTable.NewRow();
                dr[0] = ObjectID;
                Frm_ReadInfo.objectTimingDataTable.Rows.Add(dr);

                DataRow[] objectTimingResult = Frm_ReadInfo.objectTimingDataTable.Select("ObjectID = '" + ObjectID + "'");
                if (objectTimingResult.Length <= 2)
                {
                    DataRow[] objectResult = Frm_ReadInfo.objectDataTable.Select("ObjectID = '" + ObjectID + "'");
                    if (objectResult.Length > 1)
                    {
                        Utility.WriteLog("Checking ObjectID: " + ObjectID);
                        DateTime? dateTimeFirst = null;
                        DateTime? dateTimeSecond = null;
                        int index = Frm_ReadInfo.objectDataTable.Rows.IndexOf(rowData);

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
                        Utility.WriteLog("Found difference in time: " + difference);
                        Frm_ReadInfo.objectDataTable.Rows[index][8] = difference.ToString();
                    }
                }
            }

            // delete unwanted records
            foreach (DataRow rowDetails in Frm_ReadInfo.objectDataTable.Rows)
            {
                string ObjectID = rowDetails["ObjectID"].ToString();
                string ObjectType = rowDetails["ObjectType"].ToString();
                string IntervalTime = rowDetails["IntervalTime"].ToString();
                int index = Frm_ReadInfo.objectDataTable.Rows.IndexOf(rowDetails);
                int Number = Convert.ToInt32(rowDetails["Number"].ToString());
                string SpellID = rowDetails["SpellID"].ToString();
                string CastFlags = rowDetails["CastFlags"].ToString();
                string CastFlagsEx = rowDetails["CastFlagsEx"].ToString();

                DataRow[] spellIDResult = Frm_ReadInfo.objectDataTable.Select("SpellID = '" + SpellID + "'");
                foreach (DataRow rowDetails_ in spellIDResult)
                {
                    string ObjectID_ = rowDetails_["ObjectID"].ToString();
                    string SpellID_ = rowDetails_["SpellID"].ToString();
                    string CastFlags_ = rowDetails_["CastFlags"].ToString();
                    string CastFlagsEx_ = rowDetails_["CastFlagsEx"].ToString();
                    int Number_ = Convert.ToInt32(rowDetails_["Number"].ToString());
                    //int index_ = Frm_ReadInfo.objectDataTable.Rows.IndexOf(rowDetails_);

                    if (Number != Number_)
                    {
                        if (ObjectID == ObjectID_)
                        {
                            if (SpellID_ == SpellID)
                            {
                                if (CastFlags_ == CastFlags)
                                {
                                    if (CastFlagsEx_ == CastFlagsEx)
                                    {
                                        if (!deleteList.Contains(Number))
                                        {
                                            deleteList.Add(Number);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                /*if (IntervalTime == "")
                    deleteList.Add(index);*/
            }

            foreach (var deleteItemNumber in deleteList)
            {
                try
                {
                    DataRow[] row = Frm_ReadInfo.objectDataTable.Select("Number = '" + deleteItemNumber + "'");
                    //DataRow rowDetails in Frm_ReadInfo.objectDataTable.Rows
                    for (int i = row.Length - 1; i >= 0; i--)
                    {
                        Utility.WriteLog("Delete: " + deleteItemNumber);
                        row[i].Delete();
                    }
                }
                catch (Exception ex)
                {
                    Utility.WriteLog(ex.ToString());
                }
            }
        }
    }
}
