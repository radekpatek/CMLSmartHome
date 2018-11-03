-- --------------------------------------------------------
-- Hostitel:                     127.0.0.1
-- Verze serveru:                10.3.9-MariaDB - mariadb.org binary distribution
-- OS serveru:                   Win64
-- HeidiSQL Verze:               9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Exportování struktury databáze pro
CREATE DATABASE IF NOT EXISTS `SmartHome` /*!40100 DEFAULT CHARACTER SET cp1250 COLLATE cp1250_czech_cs */;
USE `SmartHome`;

-- Exportování struktury pro tabulka smarthome.collectors
CREATE TABLE IF NOT EXISTS `Collectors` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` longtext COLLATE cp1250_czech_cs DEFAULT NULL,
  `IPAddress` longtext COLLATE cp1250_czech_cs DEFAULT NULL,
  `MACAddress` longtext COLLATE cp1250_czech_cs DEFAULT NULL,
  `Description` longtext COLLATE cp1250_czech_cs DEFAULT NULL,
  `SmartHomeControllerId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Collectors_SmartHomeControllerId` (`SmartHomeControllerId`),
  CONSTRAINT `FK_Collectors_Controller_SmartHomeControllerId` FOREIGN KEY (`SmartHomeControllerId`) REFERENCES `controller` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
-- Exportování struktury pro tabulka smarthome.controller
CREATE TABLE IF NOT EXISTS `Controller` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` longtext COLLATE cp1250_czech_cs DEFAULT NULL,
  `IPAddress` longtext COLLATE cp1250_czech_cs DEFAULT NULL,
  `MACAddress` longtext COLLATE cp1250_czech_cs DEFAULT NULL,
  `Description` longtext COLLATE cp1250_czech_cs DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
-- Exportování struktury pro tabulka smarthome.sensorrecords
CREATE TABLE IF NOT EXISTS `SensorRecords` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `SensorId` bigint(20) NOT NULL,
  `CollectorId` bigint(20) NOT NULL,
  `Value` double NOT NULL,
  `Unit` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=108 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
-- Exportování struktury pro tabulka smarthome.sensors
CREATE TABLE IF NOT EXISTS `Sensors` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` longtext COLLATE cp1250_czech_cs DEFAULT NULL,
  `Type` int(11) NOT NULL,
  `CollectorId` bigint(20) DEFAULT NULL,
  `Unit` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `IX_Sensors_CollectorId` (`CollectorId`),
  CONSTRAINT `FK_Sensors_Collectors_CollectorId` FOREIGN KEY (`CollectorId`) REFERENCES `collectors` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
-- Exportování struktury pro tabulka smarthome.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(95) COLLATE cp1250_czech_cs NOT NULL,
  `ProductVersion` varchar(32) COLLATE cp1250_czech_cs NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
