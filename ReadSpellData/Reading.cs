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
    class Reading
    {
        public static void SetupDataTable()
        {
            string colObject = "ObjectID,ObjectType,CasterGUID,SpellID,CastFlags,CastFlagsEx,CasterTargetID,CasterTarget,Time,IntervalTime,Number";
            // Setup Columns for for objectDataTable
            string[] columns = colObject.Split(new char[] { ',' });
            foreach (var column in columns)
                Frm_ReadInfo.objectDataTable.Columns.Add(column);

            string colObjectTiming = "ObjectID";
            // Setup Columns for for objectTimingDataTable
            string[] columnsTiming = colObjectTiming.Split(new char[] { ',' });
            foreach (var column in columnsTiming)
                Frm_ReadInfo.objectTimingDataTable.Columns.Add(column);
        }

        public static UInt32 GetBuild(string fileName)
        {
            string line = "";
            UInt32 counter = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                // The build should be in the first few lines if it was detected. Don't read whole file.
                if (counter > 10)
                    break;

                if (line.Contains("# Detected build: "))
                {
                    string buildString = "";
                    line = line.Replace("# Detected build: ", "");
                    foreach (char chr in line)
                    {
                        if (Char.IsDigit(chr))
                            buildString += chr;
                        else
                            buildString = "";
                    }

                    UInt32 build = 0;
                    if (UInt32.TryParse(buildString, out build))
                        return build;
                }
                counter++;
            }

            return 0;
        }

        public static void GetCreatureSpells(string fileName)
        {
            Frm_ReadInfo.objectDataTable.Rows.Clear();
            var lines = File.ReadAllLines(fileName);
            ObjectStructure.SMSG_SPELL_GO sniff;

            sniff.ObjectID = "";
            sniff.ObjectType = "";
            sniff.CasterGUID = "";
            sniff.CasterUnit = "";
            sniff.SpellID = "";
            sniff.CastFlags = "";
            sniff.CastFlagsEx = "";
            sniff.CasterTarget = "";
            sniff.CasterTargetID = "";
            sniff.Time = "";

            Utility.WriteLog("Reading SMSG_SPELL_GO packets...");

            int ItemIndex = 0;
            for (int i = 1; i < lines.Count(); i++)
            {
                if (lines[i].Contains("SMSG_SPELL_GO"))
                {
                    string[] values = lines[i].Split(new char[] { ' ' });
                    string[] time = values[9].Split(new char[] { '.' });
                    sniff.Time = time[0];

                    do
                    {
                        i++;

                        if (lines[i].Contains("(Cast) CasterGUID: Full:"))
                        {
                            if (!lines[i].Contains("Player/0") & !lines[i].Contains("Item/0") & !lines[i].Contains("Pet/0"))
                            {
                                string[] packetline = lines[i].Split(new char[] { ' ' });
                                sniff.CasterGUID = packetline[3];
                                sniff.ObjectType = packetline[4];
                                sniff.ObjectID = packetline[9];
                            }
                            else
                            {
                                sniff.ObjectID = "";
                                sniff.CasterGUID = "";
                                sniff.CasterUnit = "";
                                sniff.SpellID = "";
                                sniff.CastFlags = "";
                                sniff.CastFlagsEx = "";
                                sniff.CasterTarget = "";
                                sniff.Time = "";
                                break;
                            }
                        }

                        if (lines[i].Contains("(Cast) SpellID:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });
                            sniff.SpellID = packetline[2];
                        }

                        if (lines[i].Contains("(Cast) CastFlags:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });
                            sniff.CastFlags = packetline[2];
                        }

                        if (lines[i].Contains("(Cast) CastFlagsEx:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });
                            sniff.CastFlagsEx = packetline[2];
                        }

                        if (lines[i].Contains("(Cast) [0] HitTarget: Full:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });

                            if (packetline.Length > 5)
                            {
                                sniff.CasterTarget = packetline[5];
                                sniff.CasterTargetID = packetline[10];
                            }
                        }

                    } while (lines[i] != "");
                }

                if (sniff.ObjectID != "")
                {
                    Utility.WriteLog("Found entry: " + sniff.ObjectID + " Spell: " + sniff.SpellID);
                    DataRow dr = Frm_ReadInfo.objectDataTable.NewRow();
                    ItemIndex += 1;
                    dr[0] = sniff.ObjectID;
                    dr[1] = sniff.ObjectType;
                    dr[2] = sniff.CasterGUID;
                    dr[3] = sniff.SpellID;
                    dr[4] = sniff.CastFlags;
                    dr[5] = sniff.CastFlagsEx;
                    dr[6] = sniff.CasterTarget;
                    dr[7] = sniff.CasterTargetID;
                    dr[8] = sniff.Time;
                    dr[10] = ItemIndex;
                    Frm_ReadInfo.objectDataTable.Rows.Add(dr);
                    sniff.ObjectID = "";
                    sniff.ObjectType = "";
                    sniff.SpellID = "";
                    sniff.CastFlags = "";
                    sniff.CastFlagsEx = "";
                    sniff.CasterTarget = "";
                    sniff.CasterTargetID = "";
                    sniff.Time = "";
                }
            }
        }
    }
}
