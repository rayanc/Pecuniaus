CREATE DATABASE  IF NOT EXISTS `avz` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `avz`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: localhost    Database: avz
-- ------------------------------------------------------
-- Server version	5.6.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tmp_tb_merchants`
--

DROP TABLE IF EXISTS `tmp_tb_merchants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tmp_tb_merchants` (
  `MerchantId_TMP` bigint(20) NOT NULL,
  `CompanyId` smallint(6) NOT NULL,
  `LegalName` varchar(200) NOT NULL,
  `BusinessName` varchar(200) NOT NULL,
  `WorkflowId` smallint(6) NOT NULL,
  `AddressId` bigint(20) NOT NULL,
  `AddressId2` bigint(20) DEFAULT NULL,
  `StatusId` int(11) NOT NULL,
  `StatusModifyDate` datetime DEFAULT NULL,
  `SalesRepId` bigint(20) NOT NULL,
  `BusinessStartDate` date NOT NULL,
  `IndustryTypeId` int(11) NOT NULL,
  `BusinessTypeId` int(11) NOT NULL,
  `LastActivityDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `OwnerId` bigint(20) NOT NULL,
  `BankInfoId` bigint(20) NOT NULL,
  `CNetProcessorId` varchar(20) DEFAULT NULL,
  `VNetProcessoIdd` varchar(200) DEFAULT NULL,
  `BusinessWebSite` varchar(500) DEFAULT NULL,
  `RentFlag` tinyint(1) NOT NULL,
  `LandlordName` varchar(100) DEFAULT NULL,
  `RentedAmount` double NOT NULL,
  `currencyId` int(11) DEFAULT NULL,
  `RNCNumber` varchar(11) NOT NULL,
  `TelePhone1` varchar(12) DEFAULT NULL,
  `TelePhone2` varchar(12) DEFAULT NULL,
  `IsProcessed` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`MerchantId_TMP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tmp_tb_merchants`
--

LOCK TABLES `tmp_tb_merchants` WRITE;
/*!40000 ALTER TABLE `tmp_tb_merchants` DISABLE KEYS */;
INSERT INTO `tmp_tb_merchants` VALUES (1,1,'test','test',1,1,NULL,10001,NULL,1,'2002-02-02',1,1,NULL,1,'2014-08-31 00:00:00',1,'2014-08-31 00:00:00',1,1,NULL,NULL,NULL,0,NULL,0,NULL,'11111111111',NULL,NULL,NULL),(2,1,'test','test',1,1,NULL,10001,NULL,1,'2002-02-02',1,1,NULL,1,'2014-08-31 00:00:00',1,'2014-08-31 00:00:00',1,1,NULL,NULL,NULL,0,NULL,0,NULL,'11111111111',NULL,NULL,NULL);
/*!40000 ALTER TABLE `tmp_tb_merchants` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-09-05 14:56:54