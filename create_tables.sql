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