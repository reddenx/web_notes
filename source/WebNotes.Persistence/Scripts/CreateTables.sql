﻿CREATE TABLE `account` (
  `AccountId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`AccountId`),
  UNIQUE KEY `AccountId_UNIQUE` (`AccountId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `account_credential` (
  `AccountCredentialId` int(11) NOT NULL AUTO_INCREMENT,
  `PasswordHash` binary(64) NOT NULL,
  `PasswordSalt` binary(32) NOT NULL,
  PRIMARY KEY (`AccountCredentialId`),
  UNIQUE KEY `AccountCredentialId_UNIQUE` (`AccountCredentialId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `auth_token` (
  `AuthTokenId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `AuthToken` binary(32) DEFAULT NULL,
  `AccountId` int(11) DEFAULT NULL,
  `ExpirationDate` datetime(3) DEFAULT NULL,
  PRIMARY KEY (`AuthTokenId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
