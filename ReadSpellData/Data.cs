using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions; // Regex
using System.Globalization;

namespace ReadSpellData
{
    struct SpellCastData
    {
        public UInt32 casterId;
        public String casterType;
        public UInt32 spellId;
        public UInt32 castFlags;
        public UInt32 castFlagsEx;
        public UInt32 targetId;
        public String targetType;
    };
    struct SpellCastTimes
    {
        public UInt32 casterId;
        public String casterType;
        public string casterGuid;
        public UInt32 spellId;
        public DateTime castTime;
    };
    struct SpellCooldownKey
    {
        public SpellCooldownKey(UInt32 caster, String type, UInt32 spell)
        {
            casterId = caster;
            casterType = type;
            spellId = spell;
        }
        public UInt32 casterId;
        public String casterType;
        public UInt32 spellId;
    }
    struct SpellCooldownData
    {
        public UInt32 cooldownMin;
        public UInt32 cooldownMax;
    }
    
    class Data
    {
        public static Dictionary<SpellCooldownKey, SpellCooldownData> spellCooldowns = new Dictionary<SpellCooldownKey, SpellCooldownData>();
        public static List<SpellCastData> castsList = new List<SpellCastData>();
        public static List<SpellCastTimes> castTimesList = new List<SpellCastTimes>();
        public static string fileName = "";
        public static void ParseData()
        {
            // Remove any old data.
            spellCooldowns.Clear();
            castsList.Clear();
            castTimesList.Clear();

            foreach (DataRow rowData in Frm_ReadInfo.objectDataTable.Rows)
            {
                bool found = false;
                UInt32 rowObjectID = 0;
                string rowObjectType = rowData["ObjectType"].ToString();
                UInt32 rowSpellID = 0;
                UInt32 rowCastFlags = 0;
                UInt32 rowCastFlagsEx = 0;
                string rowCasterTargetType = rowData["CasterTargetID"].ToString();
                UInt32 rowCasterTargetID = 0;
                string rowTime = rowData["Time"].ToString();
                string rowCasterGuid = rowData["CasterGUID"].ToString();

                UInt32.TryParse(rowData["ObjectID"].ToString(), out rowObjectID);
                UInt32.TryParse(rowData["SpellID"].ToString(), out rowSpellID);
                UInt32.TryParse(rowData["CastFlags"].ToString(), out rowCastFlags);
                UInt32.TryParse(rowData["CastFlagsEx"].ToString(), out rowCastFlagsEx);
                UInt32.TryParse(rowData["CasterTarget"].ToString(), out rowCasterTargetID);

                // Record time of all casts.
                SpellCastTimes castTimeData;
                castTimeData.casterId = rowObjectID;
                castTimeData.casterType = rowObjectType;
                castTimeData.casterGuid = rowCasterGuid;
                castTimeData.spellId = rowSpellID;
                castTimeData.castTime = DateTime.ParseExact(rowTime, "HH:mm:ss", CultureInfo.InvariantCulture);
                castTimesList.Add(castTimeData);

                SpellCooldownKey cdKey = new SpellCooldownKey(rowObjectID, rowObjectType, rowSpellID);
                // Make sure the cooldown dictionary contains all the spell ids.
                if (!spellCooldowns.ContainsKey(cdKey))
                {
                    SpellCooldownData tmpCooldownData;
                    tmpCooldownData.cooldownMin = UInt32.MaxValue;
                    tmpCooldownData.cooldownMax = UInt32.MinValue;
                    spellCooldowns.Add(cdKey, tmpCooldownData);
                }

                // Skip cast data we already have.
                foreach (SpellCastData listData in castsList)
                {
                    if (listData.casterId == rowObjectID &&
                        listData.casterType == rowObjectType &&
                        listData.spellId == rowSpellID &&
                        listData.castFlags == rowCastFlags &&
                        listData.castFlagsEx == rowCastFlagsEx &&
                        ((listData.targetId == rowCasterTargetID && listData.targetType == rowCasterTargetType) || (listData.targetType.Contains("Player") && rowCasterTargetType.Contains("Player"))))
                    {
                        Console.WriteLine("skip");
                        found = true;
                        break;
                    }
                }

                if (found == true)
                    continue;

                // This cast contains unique data that we want to know about.
                SpellCastData uniqueCastData;
                uniqueCastData.casterId = rowObjectID;
                uniqueCastData.casterType = rowObjectType;
                uniqueCastData.spellId = rowSpellID;
                uniqueCastData.castFlags = rowCastFlags;
                uniqueCastData.castFlagsEx = rowCastFlagsEx;
                uniqueCastData.targetId = rowCasterTargetID;
                uniqueCastData.targetType = rowCasterTargetType;
                castsList.Add(uniqueCastData);
            }

            // Iterate all the casts and calculate cooldowns.
            for (int i = 0; i < castTimesList.Count; i++)
            {
                SpellCastTimes currentCast = castTimesList[i];
                for (int j = i + 1; j < castTimesList.Count; j++)
                {
                    SpellCastTimes nextCast = castTimesList[j];
                    if (nextCast.casterGuid == currentCast.casterGuid &&
                        nextCast.spellId == currentCast.spellId)
                    {
                        SpellCooldownKey cdKey = new SpellCooldownKey(currentCast.casterId, currentCast.casterType, currentCast.spellId);
                        System.TimeSpan diff = nextCast.castTime.Subtract(currentCast.castTime);
                        SpellCooldownData tempCooldownData = spellCooldowns[cdKey];
                        if (tempCooldownData.cooldownMin > diff.TotalSeconds)
                            tempCooldownData.cooldownMin = (UInt32)diff.TotalSeconds;
                        if (tempCooldownData.cooldownMax < diff.TotalSeconds)
                            tempCooldownData.cooldownMax = (UInt32)diff.TotalSeconds;
                        spellCooldowns[cdKey] = tempCooldownData;
                        break;
                    }
                }
            }

            // Remove rows from spell cooldowns table if the cast was seen only once, and thus cooldown data has default values.
            foreach (var item in spellCooldowns.Where(kvp => kvp.Value.cooldownMin == UInt32.MaxValue).ToList())
            {
                spellCooldowns.Remove(item.Key);
            }
        }
        public static string GenerateSaveString()
        {
            string query = "";
            if (castsList.Count > 0)
            {
                query += "DELETE FROM `sniff_spell_casts` WHERE `sniffName`='" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(fileName) + "';\n";
                query += "REPLACE INTO `sniff_spell_casts` (`casterId`, `casterType`, `spellId`, `castFlags`, `castFlagsEx`, `targetId`, `targetType`, `sniffName`) VALUES";
                UInt32 count = 0;
                foreach (SpellCastData listData in castsList)
                {
                    if (count > 0)
                        query += ",";
                    query += "\n(" + listData.casterId + ", '" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(listData.casterType.Replace("/0", "")) + "', " + listData.spellId + ", " + listData.castFlags + ", " + listData.castFlagsEx + ", " + listData.targetId + ", '" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(listData.targetType.Replace("/0", "")) + "', '" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(fileName) + "')";
                    count++;
                }
                query += ";\n";
            }
            if (spellCooldowns.Count > 0)
            {
                query += "DELETE FROM `sniff_spell_cooldowns` WHERE `sniffName`='" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(fileName) + "';\n";
                query += "REPLACE INTO `sniff_spell_cooldowns` (`casterId`, `casterType`, `spellId`, `cooldownMin`, `cooldownMax`, `sniffName`) VALUES";
                UInt32 count = 0;
                foreach (KeyValuePair<SpellCooldownKey, SpellCooldownData> entry in spellCooldowns)
                {
                    if (count > 0)
                        query += ",";
                    query += "\n(" + entry.Key.casterId + ", '" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(entry.Key.casterType.Replace("/0", "")) + "', " + entry.Key.spellId + ", " + entry.Value.cooldownMin + ", " + entry.Value.cooldownMax + ", '" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(fileName) + "')";
                    count++;
                }
                query += ";\n";
            }
            return query;
        }
    }
}
