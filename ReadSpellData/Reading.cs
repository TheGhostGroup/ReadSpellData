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

        public static void GetCreatureSpellsCata(string fileName)
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

                        if (lines[i].Contains("Caster GUID: Full:"))
                        {
                            if (lines[i].Contains("Type: Creature") || lines[i].Contains("Type: GameObject"))
                            {
                                string[] packetline = lines[i].Split(new char[] { ' ' });
                                sniff.CasterGUID = packetline[3];
                                sniff.ObjectType = packetline[5];
                                sniff.ObjectID = packetline[7];
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

                        if (lines[i].Contains("Spell ID:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });
                            sniff.SpellID = packetline[2];
                        }

                        if (lines[i].Contains("Cast Flags:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });
                            sniff.CastFlags = packetline[2];
                        }

                        if (lines[i].Contains("[0] Hit GUID: Full:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });

                            if (packetline.Length > 5)
                            {
                                sniff.CasterTarget = packetline[6];
                                sniff.CasterTargetID = packetline[8];
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

        public static void GetCreatureSpellsClassic(string fileName)
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

        public static void GetPetSpellsClassic(string fileName)
        {
            Data.petSpellsMap.Clear();
            var lines = File.ReadAllLines(fileName);
            Utility.WriteLog("Reading SMSG_PET_SPELLS_MESSAGE packets...");

            for (int i = 1; i < lines.Count(); i++)
            {
                if (lines[i].Contains("SMSG_PET_SPELLS_MESSAGE"))
                {
                    UInt32 currentCreatureId = 0;
                    PetSpellData[] spellData = new PetSpellData[10];

                    do
                    {
                        i++;

                        if (lines[i].Contains("PetGUID: Full:"))
                        {
                            if (lines[i].Contains("Creature/0"))
                            {
                                string[] packetline = lines[i].Split(new char[] { ' ' });
                                string id = packetline[8];
                                UInt32.TryParse(id, out currentCreatureId);
                            }
                        }

                        if (lines[i].Contains("(ActionButtons)") && lines[i].Contains("Action:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });

                            string indexString = packetline[1].Replace("[", "").Replace("]", "");
                            UInt32 index = 0;
                            bool okIndex = UInt32.TryParse(indexString, out index);
                            string actionString = packetline[3];
                            UInt32 action = 0;
                            bool okAction = UInt32.TryParse(actionString, out action);

                            if (okIndex && okAction)
                            {
                                spellData[index].action = action;
                            }
                        }

                        if (lines[i].Contains("(ActionButtons)") && lines[i].Contains("SpellID:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });

                            string indexString = packetline[1].Replace("[", "").Replace("]", "");
                            UInt32 index = 0;
                            bool okIndex = UInt32.TryParse(indexString, out index);
                            string actionString = packetline[3];
                            UInt32 spellId = 0;
                            bool okSpell = UInt32.TryParse(actionString, out spellId);

                            if (okIndex && okSpell)
                            {
                                spellData[index].spellId = spellId;
                            }
                        }

                    } while (lines[i] != "");

                    if (currentCreatureId != 0)
                    {
                        if (!Data.petSpellsMap.ContainsKey(currentCreatureId))
                            Data.petSpellsMap.Add(currentCreatureId, spellData);
                    }
                }
            }
        }

        public static void GetPetCooldownsClassic(string fileName)
        {
            Data.petSpellCooldownsList.Clear();
            var lines = File.ReadAllLines(fileName);

            UInt32 currentCreatureId = 0;
            List<PetSpellCooldown> currentList = null;
            PetSpellCooldown cooldownData;
            cooldownData.index = 0;
            cooldownData.spellId = 0;
            cooldownData.cooldown = 0;

            Utility.WriteLog("Reading SMSG_SPELL_COOLDOWN packets...");

            for (int i = 1; i < lines.Count(); i++)
            {
                if (lines[i].Contains("SMSG_SPELL_COOLDOWN"))
                {
                    do
                    {
                        i++;

                        if (lines[i].Contains("Caster: Full:"))
                        {
                            if (lines[i].Contains("Creature/0"))
                            {
                                string[] packetline = lines[i].Split(new char[] { ' ' });
                                string id = packetline[8];
                                
                                if (UInt32.TryParse(id, out currentCreatureId))
                                {
                                    currentList = new List<PetSpellCooldown>();
                                }
                            }
                        }

                        if (lines[i].Contains("SrecID:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });

                            string indexString = packetline[0].Replace("[", "").Replace("]", "");
                            UInt32 index = 0;
                            bool okIndex = UInt32.TryParse(indexString, out index);
                            string spellIdString = packetline[2];
                            UInt32 spellId = 0;
                            bool okSpell = UInt32.TryParse(spellIdString, out spellId);

                            if (okIndex && okSpell)
                            {
                                if (index != cooldownData.index)
                                {
                                    currentList.Add(cooldownData);
                                    cooldownData.index = 0;
                                    cooldownData.spellId = 0;
                                    cooldownData.cooldown = 0;
                                }

                                cooldownData.index = index;
                                cooldownData.spellId = spellId;
                            }
                        }

                        if (lines[i].Contains("ForcedCooldown:"))
                        {
                            string[] packetline = lines[i].Split(new char[] { ' ' });
                            string cooldown = packetline[2];
                            UInt32.TryParse(cooldown, out cooldownData.cooldown);
                        }

                    } while (lines[i] != "");

                    if (currentCreatureId != 0)
                    {
                        if (cooldownData.spellId != 0)
                        {
                            currentList.Add(cooldownData);
                            cooldownData.index = 0;
                            cooldownData.spellId = 0;
                            cooldownData.cooldown = 0;
                        }

                        Data.petSpellCooldownsList.Add(new Tuple<UInt32, List<PetSpellCooldown>>(currentCreatureId, currentList));
                        currentCreatureId = 0;
                        currentList = null;
                    }
                }
            }
        }
    }
}
