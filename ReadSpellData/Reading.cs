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
        public static void GetCreatureSpells()
        {
            var lines = File.ReadAllLines(Program.fileName);
            CreatureStructure.SMSG_SPELL_GO sniff;

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

            Console.WriteLine("Reading SMSG_SPELL_GO packets...");

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
                            if (!lines[i].Contains("Player/0") & !lines[i].Contains("Item/0"))
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
                    DataRow[] result = Program.creatureDataTable.Select("SpellID = '" + sniff.SpellID + "'");
                    if (result.Length == 0)
                    {
                        Console.WriteLine("Found: " + sniff.ObjectID + " GUID: " + sniff.CasterGUID);
                        DataRow dr = Program.creatureDataTable.NewRow();

                        dr[0] = sniff.ObjectID;
                        dr[1] = sniff.ObjectType;
                        dr[2] = sniff.CasterGUID;
                        dr[3] = sniff.SpellID;
                        dr[4] = sniff.CastFlags;
                        dr[5] = sniff.CastFlagsEx;
                        dr[6] = sniff.CasterTarget;
                        dr[7] = sniff.CasterTargetID;
                        dr[8] = sniff.Time;
                        Program.creatureDataTable.Rows.Add(dr);
                        sniff.ObjectID = "";
                        sniff.ObjectType = "";
                        sniff.CasterGUID = "";
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
}
