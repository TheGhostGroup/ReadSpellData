CREATE TABLE IF NOT EXISTS `sniff_spell_casts` (
  `casterId` int(10) unsigned NOT NULL DEFAULT '0',
  `casterType` varchar(64) NOT NULL DEFAULT '',
  `spellId` int(10) unsigned NOT NULL DEFAULT '0',
  `castFlags` int(10) unsigned NOT NULL DEFAULT '0',
  `castFlagsEx` int(10) unsigned NOT NULL DEFAULT '0',
  `targetId` int(10) unsigned NOT NULL DEFAULT '0',
  `targetType` varchar(64) NOT NULL DEFAULT '',
  `sniffName` varchar(128) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `sniff_spell_cooldowns` (
  `casterId` int(10) unsigned NOT NULL DEFAULT '0',
  `casterType` varchar(64) NOT NULL DEFAULT '',
  `spellId` int(10) unsigned NOT NULL DEFAULT '0',
  `cooldownMin` int(10) unsigned NOT NULL DEFAULT '0',
  `cooldownMax` int(10) unsigned NOT NULL DEFAULT '0',
  `sniffName` varchar(128) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
