CREATE TABLE IF NOT EXISTS `sniff_spell_casts` (
  `casterId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `casterType` varchar(64) NOT NULL DEFAULT '',
  `spellId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `castFlags` int(10) unsigned NOT NULL DEFAULT '0',
  `castFlagsEx` int(10) unsigned NOT NULL DEFAULT '0',
  `targetId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `targetType` varchar(64) NOT NULL DEFAULT '',
  `build` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniffName` varchar(128) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `sniff_spell_cooldowns` (
  `casterId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `casterType` varchar(64) NOT NULL DEFAULT '',
  `spellId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `cooldownMin` smallint(5) unsigned NOT NULL DEFAULT '0',
  `cooldownMax` smallint(5) unsigned NOT NULL DEFAULT '0',
  `build` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniffName` varchar(128) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `sniff_pet_cooldowns` (
  `creatureId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `index` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `cooldown` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `build` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniffName` varchar(128) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `sniff_pet_spells` (
  `creatureId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action1` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId1` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action2` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId2` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action3` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId3` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action4` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId4` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action5` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId5` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action6` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId6` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action7` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId7` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action8` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId8` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action9` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId9` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `action10` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellId10` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `build` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniffName` varchar(129) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
