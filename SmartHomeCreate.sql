-- --------------------------------------------------------
-- Hostitel:                     cmlsmarthomecontroller
-- Verze serveru:                10.1.48-MariaDB-0+deb9u1 - Raspbian 9.11
-- OS serveru:                   debian-linux-gnueabihf
-- HeidiSQL Verze:               11.2.0.6213
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Exportování struktury databáze pro
CREATE DATABASE IF NOT EXISTS `SmartHome` /*!40100 DEFAULT CHARACTER SET cp1250 COLLATE cp1250_czech_cs */;
USE `SmartHome`;

-- Exportování struktury pro tabulka SmartHome.Collectors
CREATE TABLE IF NOT EXISTS `Collectors` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4,
  `MACAddress` longtext CHARACTER SET utf8mb4,
  `Description` longtext CHARACTER SET utf8mb4,
  `SmartHomeControllerId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Collectors_SmartHomeControllerId` (`SmartHomeControllerId`),
  CONSTRAINT `FK_Collectors_Controller_SmartHomeControllerId` FOREIGN KEY (`SmartHomeControllerId`) REFERENCES `Controller` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.Collectors: ~4 rows (přibližně)
DELETE FROM `Collectors`;
/*!40000 ALTER TABLE `Collectors` DISABLE KEYS */;
INSERT INTO `Collectors` (`Id`, `Name`, `MACAddress`, `Description`, `SmartHomeControllerId`) VALUES
	(1, 'Jidelna', 'BCDDC230C2B2', 'Teplota, vlhkost a CO2 v jídelně', 1),
	(2, 'Zahradmí domek', 'BCDDC230BF08', 'Teplota, vlhkost v zahradním domku', 1),
	(3, 'Loznice', 'BCDDC2233403', 'Teplota, vlhkost a CO2 v ložnici', 1),
	(4, 'Pokojicek', 'D8BFC010D639', 'Teplota, vlhkost a CO2 v pokojíčku', 1);
/*!40000 ALTER TABLE `Collectors` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.Controller
CREATE TABLE IF NOT EXISTS `Controller` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4,
  `MACAddress` longtext CHARACTER SET utf8mb4,
  `Description` longtext CHARACTER SET utf8mb4,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.Controller: ~0 rows (přibližně)
DELETE FROM `Controller`;
/*!40000 ALTER TABLE `Controller` DISABLE KEYS */;
INSERT INTO `Controller` (`Id`, `Name`, `MACAddress`, `Description`) VALUES
	(1, 'cmlsmarthomecontroller', 'B827EB2DC7D5', 'Main SmartHome Controller');
/*!40000 ALTER TABLE `Controller` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.Dashboard
CREATE TABLE IF NOT EXISTS `Dashboard` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `OutdoorCollectorId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Dashboard_OutdoorCollectorId` (`OutdoorCollectorId`),
  CONSTRAINT `FK_Dashboard_Collectors_OutdoorCollectorId` FOREIGN KEY (`OutdoorCollectorId`) REFERENCES `Collectors` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.Dashboard: ~0 rows (přibližně)
DELETE FROM `Dashboard`;
/*!40000 ALTER TABLE `Dashboard` DISABLE KEYS */;
INSERT INTO `Dashboard` (`Id`, `OutdoorCollectorId`) VALUES
	(1, 2);
/*!40000 ALTER TABLE `Dashboard` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.SensorRecords
CREATE TABLE IF NOT EXISTS `SensorRecords` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `SensorId` bigint(20) NOT NULL,
  `CollectorId` bigint(20) NOT NULL,
  `Value` double NOT NULL,
  `Unit` int(11) NOT NULL,
  `DateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_SensorRecords_SensorId_DateTime` (`SensorId`,`DateTime`),
  KEY `IX_SensorRecords_DateTime_SensorId` (`DateTime`,`SensorId`),
  KEY `IX_SensorRecords_CollectorId_SensorId_DateTime` (`CollectorId`,`SensorId`,`DateTime`)
) ENGINE=InnoDB AUTO_INCREMENT=778959 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.SensorRecords: ~0 rows (přibližně)
DELETE FROM `SensorRecords`;
/*!40000 ALTER TABLE `SensorRecords` DISABLE KEYS */;
/*!40000 ALTER TABLE `SensorRecords` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.Sensors
CREATE TABLE IF NOT EXISTS `Sensors` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4,
  `Type` int(11) NOT NULL,
  `Unit` int(11) NOT NULL,
  `CollectorId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Sensors_CollectorId` (`CollectorId`),
  CONSTRAINT `FK_Sensors_Collectors_CollectorId` FOREIGN KEY (`CollectorId`) REFERENCES `Collectors` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.Sensors: ~11 rows (přibližně)
DELETE FROM `Sensors`;
/*!40000 ALTER TABLE `Sensors` DISABLE KEYS */;
INSERT INTO `Sensors` (`Id`, `Name`, `Type`, `Unit`, `CollectorId`) VALUES
	(1, 'Teplota v jídelně', 1, 1, 1),
	(2, 'Vlhkost v jídelně', 2, 2, 1),
	(3, 'CO2 v jídelně', 3, 3, 1),
	(4, 'Teplota v zahradním domku', 1, 1, 2),
	(5, 'Vlhkost v zahradním domku', 2, 2, 2),
	(6, 'Teplota v ložnici', 1, 1, 3),
	(7, 'Vlhkost v ložnici', 2, 2, 3),
	(8, 'CO2 v ložnici', 3, 3, 3),
	(9, 'Teplota v pokojíčku', 1, 1, 4),
	(10, 'Vlhkost v pokojíčku', 2, 2, 4),
	(11, 'CO2 v pokojíčku', 3, 3, 4);
/*!40000 ALTER TABLE `Sensors` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.WeatherForecast
CREATE TABLE IF NOT EXISTS `WeatherForecast` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `DateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8439 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.WeatherForecast: ~1 rows (přibližně)
DELETE FROM `WeatherForecast`;
/*!40000 ALTER TABLE `WeatherForecast` DISABLE KEYS */;
INSERT INTO `WeatherForecast` (`Id`, `DateTime`) VALUES
	(8438, '2021-03-15 11:00:00.628345');
/*!40000 ALTER TABLE `WeatherForecast` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.WeatherForecastCurrentState
CREATE TABLE IF NOT EXISTS `WeatherForecastCurrentState` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `WeatherForecastId` bigint(20) NOT NULL,
  `DateTime` datetime(6) NOT NULL,
  `AtmosphericTemperature` double NOT NULL,
  `Pressure` int(11) NOT NULL,
  `Humidity` int(11) NOT NULL,
  `Cloudiness` int(11) NOT NULL,
  `WindSpeed` double NOT NULL,
  `WindDirection` int(11) NOT NULL,
  `SunriseTime` datetime(6) NOT NULL,
  `SunsetTime` datetime(6) NOT NULL,
  `Temperature` double NOT NULL,
  `FeelsLikeTemperature` double NOT NULL,
  `UVIndex` double NOT NULL,
  `AverageVisibility` int(11) NOT NULL,
  `Rain` double DEFAULT NULL,
  `Snow` double DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_WeatherForecastCurrentState_WeatherForecastId` (`WeatherForecastId`),
  CONSTRAINT `FK_WeatherForecastCurrentState_WeatherForecast_WeatherForecastId` FOREIGN KEY (`WeatherForecastId`) REFERENCES `WeatherForecast` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8439 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.WeatherForecastCurrentState: ~1 rows (přibližně)
DELETE FROM `WeatherForecastCurrentState`;
/*!40000 ALTER TABLE `WeatherForecastCurrentState` DISABLE KEYS */;
INSERT INTO `WeatherForecastCurrentState` (`Id`, `WeatherForecastId`, `DateTime`, `AtmosphericTemperature`, `Pressure`, `Humidity`, `Cloudiness`, `WindSpeed`, `WindDirection`, `SunriseTime`, `SunsetTime`, `Temperature`, `FeelsLikeTemperature`, `UVIndex`, `AverageVisibility`, `Rain`, `Snow`) VALUES
	(8438, 8438, '2021-03-15 11:00:00.000000', 0.55, 1009, 65, 40, 5.14, 230, '2021-03-15 06:14:15.000000', '2021-03-15 18:04:48.000000', 6.65, 1.15, 1.37, 10000, NULL, NULL);
/*!40000 ALTER TABLE `WeatherForecastCurrentState` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.WeatherForecastDailyState
CREATE TABLE IF NOT EXISTS `WeatherForecastDailyState` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `WeatherForecastId` bigint(20) NOT NULL,
  `DateTime` datetime(6) NOT NULL,
  `AtmosphericTemperature` double NOT NULL,
  `Pressure` int(11) NOT NULL,
  `Humidity` int(11) NOT NULL,
  `Cloudiness` int(11) NOT NULL,
  `WindSpeed` double NOT NULL,
  `WindDirection` int(11) NOT NULL,
  `SunriseTime` datetime(6) NOT NULL,
  `SunsetTime` datetime(6) NOT NULL,
  `UVIndex` double NOT NULL,
  `AverageVisibility` int(11) NOT NULL,
  `Rain` double NOT NULL,
  `Snow` double NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_WeatherForecastDailyState_WeatherForecastId` (`WeatherForecastId`),
  CONSTRAINT `FK_WeatherForecastDailyState_WeatherForecast_WeatherForecastId` FOREIGN KEY (`WeatherForecastId`) REFERENCES `WeatherForecast` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=67505 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.WeatherForecastDailyState: ~8 rows (přibližně)
DELETE FROM `WeatherForecastDailyState`;
/*!40000 ALTER TABLE `WeatherForecastDailyState` DISABLE KEYS */;
INSERT INTO `WeatherForecastDailyState` (`Id`, `WeatherForecastId`, `DateTime`, `AtmosphericTemperature`, `Pressure`, `Humidity`, `Cloudiness`, `WindSpeed`, `WindDirection`, `SunriseTime`, `SunsetTime`, `UVIndex`, `AverageVisibility`, `Rain`, `Snow`) VALUES
	(67497, 8438, '2021-03-19 12:00:00.000000', -7.25, 1020, 47, 97, 4.55, 19, '2021-03-19 06:05:29.000000', '2021-03-19 18:11:09.000000', 1.93, 0, 0, 0.36),
	(67498, 8438, '2021-03-16 12:00:00.000000', -1.37, 1016, 69, 94, 8.16, 312, '2021-03-16 06:12:04.000000', '2021-03-16 18:06:23.000000', 1.56, 0, 0.11, 1.63),
	(67499, 8438, '2021-03-20 12:00:00.000000', -7.33, 1025, 42, 18, 5.11, 73, '2021-03-20 06:03:17.000000', '2021-03-20 18:12:43.000000', 2, 0, 0, 0),
	(67500, 8438, '2021-03-21 12:00:00.000000', -6.33, 1021, 40, 0, 2.87, 60, '2021-03-21 06:01:05.000000', '2021-03-21 18:14:18.000000', 2, 0, 0, 0),
	(67501, 8438, '2021-03-22 12:00:00.000000', -0.52, 1019, 54, 11, 2.96, 39, '2021-03-22 05:58:53.000000', '2021-03-22 18:15:53.000000', 2, 0, 0, 0),
	(67502, 8438, '2021-03-15 12:00:00.000000', 0.23, 1009, 62, 65, 5.34, 261, '2021-03-15 06:14:15.000000', '2021-03-15 18:04:48.000000', 1.51, 0, 3.13, 0),
	(67503, 8438, '2021-03-18 12:00:00.000000', -3.4, 1016, 58, 100, 1.92, 315, '2021-03-18 06:07:41.000000', '2021-03-18 18:09:34.000000', 2.2, 0, 0, 0.45),
	(67504, 8438, '2021-03-17 12:00:00.000000', -0.88, 1021, 69, 98, 4.82, 333, '2021-03-17 06:09:52.000000', '2021-03-17 18:07:58.000000', 1.39, 0, 0, 1.14);
/*!40000 ALTER TABLE `WeatherForecastDailyState` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.WeatherForecastDailyTemperature
CREATE TABLE IF NOT EXISTS `WeatherForecastDailyTemperature` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `DailyStateTemperatureId` bigint(20) DEFAULT NULL,
  `DailyStateFeelsLikeTemperatureId` bigint(20) DEFAULT NULL,
  `DayTemperature` double NOT NULL,
  `MinDailyTemperature` double NOT NULL,
  `MaxDailyTemperature` double NOT NULL,
  `NightTemperature` double NOT NULL,
  `EveningTemperature` double NOT NULL,
  `MorningTemperature` double NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_WeatherForecastDailyTemperature_DailyStateFeelsLikeTemperatu~` (`DailyStateFeelsLikeTemperatureId`),
  UNIQUE KEY `IX_WeatherForecastDailyTemperature_DailyStateTemperatureId` (`DailyStateTemperatureId`),
  CONSTRAINT `FK_WeatherForecastDailyTemperature_WeatherForecastDailyState_Da~` FOREIGN KEY (`DailyStateFeelsLikeTemperatureId`) REFERENCES `WeatherForecastDailyState` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_WeatherForecastDailyTemperature_WeatherForecastDailyState_D~1` FOREIGN KEY (`DailyStateTemperatureId`) REFERENCES `WeatherForecastDailyState` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=135009 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.WeatherForecastDailyTemperature: ~16 rows (přibližně)
DELETE FROM `WeatherForecastDailyTemperature`;
/*!40000 ALTER TABLE `WeatherForecastDailyTemperature` DISABLE KEYS */;
INSERT INTO `WeatherForecastDailyTemperature` (`Id`, `DailyStateTemperatureId`, `DailyStateFeelsLikeTemperatureId`, `DayTemperature`, `MinDailyTemperature`, `MaxDailyTemperature`, `NightTemperature`, `EveningTemperature`, `MorningTemperature`) VALUES
	(134993, 67503, NULL, 4.29, -0.91, 4.4, -0.91, 1.46, 0.15),
	(134994, NULL, 67501, 4.24, 0, 0, -1.84, -0.56, -3.93),
	(134995, 67501, NULL, 8.36, -1.49, 8.36, 1.58, 3.56, -0.33),
	(134996, NULL, 67503, 0.53, 0, 0, -5.41, -2.76, -3.79),
	(134997, 67498, NULL, 4, 1.51, 4.41, 1.51, 2.72, 2.37),
	(134998, NULL, 67498, -3.86, 0, 0, -3.57, -2.84, -3.53),
	(134999, NULL, 67500, 1.7, 0, 0, -4.13, -2.71, -5.57),
	(135000, NULL, 67504, -0.88, 0, 0, -3.66, -2.49, -4.76),
	(135001, 67502, NULL, 7, 1.27, 7.77, 3.02, 4.13, 1.68),
	(135002, 67504, NULL, 4.57, 0.39, 5.71, 0.39, 2.6, 0.78),
	(135003, 67499, NULL, 4.81, -2, 4.81, -1.73, -0.21, -1.76),
	(135004, NULL, 67499, -1.58, 0, 0, -6.43, -5.63, -5.64),
	(135005, 67497, NULL, 3.16, -1.9, 3.16, -0.83, -0.02, -1.81),
	(135006, NULL, 67497, -2.84, 0, 0, -4.22, -4.32, -7.75),
	(135007, NULL, 67502, 1.31, 0, 0, -2.11, 0.27, -3.4),
	(135008, 67500, NULL, 6.44, -2.45, 6.48, -0.46, 1.65, -1.46);
/*!40000 ALTER TABLE `WeatherForecastDailyTemperature` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.WeatherForecastHourlyState
CREATE TABLE IF NOT EXISTS `WeatherForecastHourlyState` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `WeatherForecastId` bigint(20) NOT NULL,
  `DateTime` datetime(6) NOT NULL,
  `AtmosphericTemperature` double NOT NULL,
  `Pressure` int(11) NOT NULL,
  `Humidity` int(11) NOT NULL,
  `Cloudiness` int(11) NOT NULL,
  `WindSpeed` double NOT NULL,
  `WindDirection` int(11) NOT NULL,
  `Temperature` double NOT NULL,
  `FeelsLikeTemperature` double NOT NULL,
  `Rain` double DEFAULT NULL,
  `Snow` double DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_WeatherForecastHourlyState_WeatherForecastId` (`WeatherForecastId`),
  CONSTRAINT `FK_WeatherForecastHourlyState_WeatherForecast_WeatherForecastId` FOREIGN KEY (`WeatherForecastId`) REFERENCES `WeatherForecast` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=405025 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.WeatherForecastHourlyState: ~48 rows (přibližně)
DELETE FROM `WeatherForecastHourlyState`;
/*!40000 ALTER TABLE `WeatherForecastHourlyState` DISABLE KEYS */;
INSERT INTO `WeatherForecastHourlyState` (`Id`, `WeatherForecastId`, `DateTime`, `AtmosphericTemperature`, `Pressure`, `Humidity`, `Cloudiness`, `WindSpeed`, `WindDirection`, `Temperature`, `FeelsLikeTemperature`, `Rain`, `Snow`) VALUES
	(404977, 8438, '2021-03-16 21:00:00.000000', -0.7, 1020, 84, 91, 4.48, 303, 2, -3, 0, 0),
	(404978, 8438, '2021-03-16 18:00:00.000000', -1.48, 1018, 75, 95, 4.86, 327, 3, -3, 0, 0),
	(404979, 8438, '2021-03-16 17:00:00.000000', -1.46, 1018, 69, 100, 6.65, 329, 4, -3, 0, 0),
	(404980, 8438, '2021-03-16 16:00:00.000000', -1.52, 1017, 67, 100, 7.38, 324, 4, -3, 0, 0),
	(404981, 8438, '2021-03-16 15:00:00.000000', -1.41, 1017, 67, 100, 7.89, 318, 4, -3, 0, 0),
	(404982, 8438, '2021-03-16 14:00:00.000000', -1.65, 1017, 66, 100, 7.99, 313, 4, -3, 0, 0),
	(404983, 8438, '2021-03-16 13:00:00.000000', -1.47, 1017, 68, 95, 7.84, 311, 4, -3, 0, 0),
	(404984, 8438, '2021-03-16 19:00:00.000000', -1.68, 1019, 79, 82, 4.04, 313, 2, -3, 0, 0),
	(404985, 8438, '2021-03-16 20:00:00.000000', -1.33, 1019, 80, 87, 4.13, 305, 2, -3, 0, 0),
	(404986, 8438, '2021-03-15 22:00:00.000000', 0.65, 1010, 88, 52, 3.85, 255, 3, -2, 0, 0),
	(404987, 8438, '2021-03-16 22:00:00.000000', -0.29, 1020, 89, 93, 4.5, 300, 2, -4, 0, 0),
	(404988, 8438, '2021-03-16 23:00:00.000000', -0.15, 1020, 90, 90, 4.43, 299, 2, -4, 0, 0),
	(404989, 8438, '2021-03-17 00:00:00.000000', -0.34, 1021, 92, 86, 4.39, 301, 1, -4, 0, 0),
	(404990, 8438, '2021-03-17 01:00:00.000000', -0.4, 1020, 94, 80, 4.16, 300, 1, -4, 0, 0),
	(404991, 8438, '2021-03-17 02:00:00.000000', -0.09, 1020, 91, 93, 4.22, 308, 2, -3, 0, 0),
	(404992, 8438, '2021-03-17 03:00:00.000000', -0.04, 1020, 90, 97, 4.01, 309, 2, -3, 0, 0),
	(404993, 8438, '2021-03-17 04:00:00.000000', 0.34, 1021, 96, 97, 3.77, 304, 1, -3, 0, 0.11),
	(404994, 8438, '2021-03-17 05:00:00.000000', 0.13, 1021, 96, 98, 4.13, 301, 1, -4, 0, 0.2),
	(404995, 8438, '2021-03-17 06:00:00.000000', -0.33, 1021, 94, 98, 5.06, 306, 1, -5, 0, 0.23),
	(404996, 8438, '2021-03-17 07:00:00.000000', -1.03, 1020, 90, 99, 4.57, 306, 1, -5, 0, 0.11),
	(404997, 8438, '2021-03-17 08:00:00.000000', -1.11, 1020, 83, 96, 4.35, 310, 2, -3, 0, 0),
	(404998, 8438, '2021-03-16 12:00:00.000000', -1.37, 1016, 69, 94, 8.16, 312, 4, -4, 0, 0),
	(404999, 8438, '2021-03-16 11:00:00.000000', -1.31, 1016, 70, 96, 7.78, 316, 4, -4, 0, 0),
	(405000, 8438, '2021-03-16 06:00:00.000000', 1.39, 1012, 95, 96, 5.96, 290, 2, -4, 0, 0),
	(405001, 8438, '2021-03-16 09:00:00.000000', 1.04, 1014, 95, 100, 5.95, 307, 2, -4, 0, 0.65),
	(405002, 8438, '2021-03-15 21:00:00.000000', 0.77, 1009, 88, 71, 3.05, 259, 3, -1, 0, 0),
	(405003, 8438, '2021-03-15 20:00:00.000000', 1.23, 1009, 87, 85, 3.02, 278, 4, 0, 0, 0),
	(405004, 8438, '2021-03-15 19:00:00.000000', 2.12, 1009, 88, 98, 2.98, 301, 4, 0, 0.86, 0),
	(405005, 8438, '2021-03-15 18:00:00.000000', 2.75, 1008, 93, 97, 3.4, 298, 4, 0, 1.79, 0),
	(405006, 8438, '2021-03-15 17:00:00.000000', 2.16, 1008, 82, 96, 3.62, 287, 5, 1, 0.48, 0),
	(405007, 8438, '2021-03-15 16:00:00.000000', 0.19, 1008, 63, 95, 3.92, 272, 7, 2, 0, 0),
	(405008, 8438, '2021-03-15 15:00:00.000000', -0.11, 1008, 58, 92, 4.94, 262, 8, 2, 0, 0),
	(405009, 8438, '2021-03-15 14:00:00.000000', -0.18, 1007, 57, 81, 5.71, 257, 8, 2, 0, 0),
	(405010, 8438, '2021-03-15 13:00:00.000000', 0.16, 1008, 59, 77, 5.72, 258, 8, 2, 0, 0),
	(405011, 8438, '2021-03-15 12:00:00.000000', 0.23, 1009, 62, 65, 5.34, 261, 7, 1, 0, 0),
	(405012, 8438, '2021-03-15 11:00:00.000000', 0.55, 1009, 65, 40, 5.18, 260, 7, 1, 0, 0),
	(405013, 8438, '2021-03-15 23:00:00.000000', 1.08, 1010, 89, 51, 4.8, 274, 3, -2, 0, 0),
	(405014, 8438, '2021-03-16 00:00:00.000000', 1.43, 1010, 87, 61, 4.87, 284, 4, -1, 0, 0),
	(405015, 8438, '2021-03-16 01:00:00.000000', 0.86, 1011, 88, 66, 4.16, 278, 3, -2, 0, 0),
	(405016, 8438, '2021-03-16 02:00:00.000000', 0.75, 1011, 88, 82, 4.53, 277, 3, -2, 0, 0),
	(405017, 8438, '2021-03-16 03:00:00.000000', 0.92, 1011, 89, 91, 5.91, 288, 3, -3, 0, 0),
	(405018, 8438, '2021-03-16 04:00:00.000000', 1.26, 1011, 93, 94, 5.67, 295, 3, -3, 0, 0),
	(405019, 8438, '2021-03-16 05:00:00.000000', 1.37, 1011, 95, 96, 5.43, 290, 2, -3, 0, 0),
	(405020, 8438, '2021-03-17 09:00:00.000000', -1.04, 1021, 78, 96, 4.36, 318, 3, -3, 0, 0),
	(405021, 8438, '2021-03-16 07:00:00.000000', 1.15, 1013, 95, 97, 6.43, 303, 2, -4, 0.11, 0),
	(405022, 8438, '2021-03-16 08:00:00.000000', 1.22, 1014, 95, 100, 5.91, 310, 2, -4, 0, 0.31),
	(405023, 8438, '2021-03-16 10:00:00.000000', 0.18, 1015, 90, 100, 7.73, 311, 2, -5, 0, 0.67),
	(405024, 8438, '2021-03-17 10:00:00.000000', -0.68, 1021, 78, 96, 4.66, 320, 3, -2, 0, 0.14);
/*!40000 ALTER TABLE `WeatherForecastHourlyState` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.WeatherForecastWeather
CREATE TABLE IF NOT EXISTS `WeatherForecastWeather` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `WeatherConditionId` int(11) NOT NULL,
  `Main` longtext CHARACTER SET utf8mb4,
  `Description` longtext CHARACTER SET utf8mb4,
  `Icon` longtext CHARACTER SET utf8mb4,
  `CurrentStateId` bigint(20) DEFAULT NULL,
  `DailyStateId` bigint(20) DEFAULT NULL,
  `HourlyStateId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_WeatherForecastWeather_CurrentStateId` (`CurrentStateId`),
  KEY `IX_WeatherForecastWeather_DailyStateId` (`DailyStateId`),
  KEY `IX_WeatherForecastWeather_HourlyStateId` (`HourlyStateId`),
  CONSTRAINT `FK_WeatherForecastWeather_WeatherForecastCurrentState_CurrentSt~` FOREIGN KEY (`CurrentStateId`) REFERENCES `WeatherForecastCurrentState` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_WeatherForecastWeather_WeatherForecastDailyState_DailyStateId` FOREIGN KEY (`DailyStateId`) REFERENCES `WeatherForecastDailyState` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_WeatherForecastWeather_WeatherForecastHourlyState_HourlyStat~` FOREIGN KEY (`HourlyStateId`) REFERENCES `WeatherForecastHourlyState` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=481109 DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.WeatherForecastWeather: ~57 rows (přibližně)
DELETE FROM `WeatherForecastWeather`;
/*!40000 ALTER TABLE `WeatherForecastWeather` DISABLE KEYS */;
INSERT INTO `WeatherForecastWeather` (`Id`, `WeatherConditionId`, `Main`, `Description`, `Icon`, `CurrentStateId`, `DailyStateId`, `HourlyStateId`) VALUES
	(481052, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 404980),
	(481053, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 404981),
	(481054, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 404983),
	(481055, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 404998),
	(481056, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 404999),
	(481057, 601, 'Snow', 'snow', '13d', NULL, NULL, 405023),
	(481058, 601, 'Snow', 'snow', '13d', NULL, NULL, 405001),
	(481059, 600, 'Snow', 'light snow', '13d', NULL, NULL, 405022),
	(481060, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 404982),
	(481061, 803, 'Clouds', 'broken clouds', '04n', NULL, NULL, 404986),
	(481062, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 404978),
	(481063, 500, 'Rain', 'light rain', '10d', NULL, NULL, 405021),
	(481064, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 404997),
	(481065, 600, 'Snow', 'light snow', '13d', NULL, NULL, 404996),
	(481066, 600, 'Snow', 'light snow', '13n', NULL, NULL, 404995),
	(481067, 600, 'Snow', 'light snow', '13n', NULL, NULL, 404994),
	(481068, 600, 'Snow', 'light snow', '13n', NULL, NULL, 404993),
	(481069, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 404992),
	(481070, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 404979),
	(481071, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 404991),
	(481072, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 404989),
	(481073, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 404988),
	(481074, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 404987),
	(481075, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 404977),
	(481076, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 404985),
	(481077, 803, 'Clouds', 'broken clouds', '04n', NULL, NULL, 404984),
	(481078, 803, 'Clouds', 'broken clouds', '04n', NULL, NULL, 404990),
	(481079, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 405000),
	(481080, 802, 'Clouds', 'scattered clouds', '03d', 8438, NULL, NULL),
	(481081, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 405018),
	(481082, 803, 'Clouds', 'broken clouds', '04n', NULL, NULL, 405002),
	(481083, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 405003),
	(481084, 500, 'Rain', 'light rain', '10n', NULL, NULL, 405004),
	(481085, 501, 'Rain', 'moderate rain', '10d', NULL, NULL, 405005),
	(481086, 500, 'Rain', 'light rain', '10d', NULL, NULL, 405006),
	(481087, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 405007),
	(481088, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 405008),
	(481089, 803, 'Clouds', 'broken clouds', '04d', NULL, NULL, 405009),
	(481090, 803, 'Clouds', 'broken clouds', '04d', NULL, NULL, 405010),
	(481091, 803, 'Clouds', 'broken clouds', '04d', NULL, NULL, 405011),
	(481092, 802, 'Clouds', 'scattered clouds', '03d', NULL, NULL, 405012),
	(481093, 801, 'Clouds', 'few clouds', '02d', NULL, 67501, NULL),
	(481094, 800, 'Clear', 'clear sky', '01d', NULL, 67500, NULL),
	(481095, 801, 'Clouds', 'few clouds', '02d', NULL, 67499, NULL),
	(481096, 600, 'Snow', 'light snow', '13d', NULL, 67497, NULL),
	(481097, 600, 'Snow', 'light snow', '13d', NULL, 67503, NULL),
	(481098, 600, 'Snow', 'light snow', '13d', NULL, 67504, NULL),
	(481099, 616, 'Snow', 'rain and snow', '13d', NULL, 67498, NULL),
	(481100, 501, 'Rain', 'moderate rain', '10d', NULL, 67502, NULL),
	(481101, 804, 'Clouds', 'overcast clouds', '04d', NULL, NULL, 405020),
	(481102, 803, 'Clouds', 'broken clouds', '04n', NULL, NULL, 405013),
	(481103, 803, 'Clouds', 'broken clouds', '04n', NULL, NULL, 405014),
	(481104, 803, 'Clouds', 'broken clouds', '04n', NULL, NULL, 405015),
	(481105, 803, 'Clouds', 'broken clouds', '04n', NULL, NULL, 405016),
	(481106, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 405017),
	(481107, 804, 'Clouds', 'overcast clouds', '04n', NULL, NULL, 405019),
	(481108, 600, 'Snow', 'light snow', '13d', NULL, NULL, 405024);
/*!40000 ALTER TABLE `WeatherForecastWeather` ENABLE KEYS */;

-- Exportování struktury pro tabulka SmartHome.__EFMigrationsHistory
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(95) COLLATE cp1250_czech_cs NOT NULL,
  `ProductVersion` varchar(32) COLLATE cp1250_czech_cs NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1250 COLLATE=cp1250_czech_cs;

-- Exportování dat pro tabulku SmartHome.__EFMigrationsHistory: ~2 rows (přibližně)
DELETE FROM `__EFMigrationsHistory`;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
	('20200419113254_InitDB', '3.1.3'),
	('20200419113940_DailyStateFeelsLikeTemperature', '3.1.3'),
	('20200419114533_InitDB', '3.1.3');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
