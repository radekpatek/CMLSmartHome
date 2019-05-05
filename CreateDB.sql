-- --------------------------------------------------------
-- Hostitel:                     192.168.1.152
-- Verze serveru:                10.1.23-MariaDB-9+deb9u1 - Raspbian 9.0
-- OS serveru:                   debian-linux-gnueabihf
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

-- Exportování struktury pro tabulka SmartHome.Collectors
CREATE TABLE IF NOT EXISTS `Collectors` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` longtext COLLATE cp1250_czech_cs,
  `MACAddress` longtext COLLATE cp1250_czech_cs,
  `Description` longtext COLLATE cp1250_czech_cs,
  `SmartHomeControllerId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Collectors_SmartHomeControllerId` (`SmartHomeControllerId`),
  CONSTRAINT `FK_Collectors_Controller_SmartHomeControllerId` FOREIGN KEY (`SmartHomeControllerId`) REFERENCES `controller` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=254 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
-- Exportování struktury pro tabulka SmartHome.Controller
CREATE TABLE IF NOT EXISTS `Controller` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` longtext COLLATE cp1250_czech_cs,
  `MACAddress` longtext COLLATE cp1250_czech_cs,
  `Description` longtext COLLATE cp1250_czech_cs,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
-- Exportování struktury pro tabulka SmartHome.Dashboard
CREATE TABLE IF NOT EXISTS `Dashboard` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `InternalCollectorId` bigint(20) DEFAULT NULL,
  `OutdoorCollectorId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Dashboard_InternalCollectorId` (`InternalCollectorId`),
  KEY `IX_Dashboard_OutdoorCollectorId` (`OutdoorCollectorId`),
  CONSTRAINT `FK_Dashboard_Collectors_InternalCollectorId` FOREIGN KEY (`InternalCollectorId`) REFERENCES `Collectors` (`Id`) ON DELETE NO ACTION,
  CONSTRAINT `FK_Dashboard_Collectors_OutdoorCollectorId` FOREIGN KEY (`OutdoorCollectorId`) REFERENCES `Collectors` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
-- Exportování struktury pro tabulka SmartHome.SensorRecords
CREATE TABLE IF NOT EXISTS `SensorRecords` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `SensorId` bigint(20) NOT NULL,
  `CollectorId` bigint(20) NOT NULL,
  `Value` double NOT NULL,
  `Unit` int(11) NOT NULL,
  `DateTime` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2456 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
-- Exportování struktury pro tabulka SmartHome.Sensors
CREATE TABLE IF NOT EXISTS `Sensors` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` longtext COLLATE cp1250_czech_cs,
  `Type` int(11) NOT NULL,
  `CollectorId` bigint(20) DEFAULT NULL,
  `Unit` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_Sensors_CollectorId` (`CollectorId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
-- Exportování struktury pro tabulka SmartHome.__EFMigrationsHistory
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(95) COLLATE cp1250_czech_cs NOT NULL,
  `ProductVersion` varchar(32) COLLATE cp1250_czech_cs NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Export dat nebyl vybrán.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
