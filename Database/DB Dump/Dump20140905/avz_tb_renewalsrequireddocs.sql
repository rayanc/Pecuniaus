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
-- Table structure for table `tb_renewalsrequireddocs`
--

DROP TABLE IF EXISTS `tb_renewalsrequireddocs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_renewalsrequireddocs` (
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `RequiredDocTypeID` int(11) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  KEY `FK_tb_RenewalsRequiredDocs_Gen_tb_DocumentTypes` (`RequiredDocTypeID`),
  KEY `FK_tb_RenewalsRequiredDocs_tb_Contracts` (`ContractID`),
  KEY `FK_tb_RenewalsRequiredDocs_tb_Merchants` (`MerchantID`),
  CONSTRAINT `FK_tb_RenewalsRequiredDocs_Gen_tb_DocumentTypes` FOREIGN KEY (`RequiredDocTypeID`) REFERENCES `lkp_tb_documenttypes` (`DocumentTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_RenewalsRequiredDocs_tb_Contracts` FOREIGN KEY (`ContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_RenewalsRequiredDocs_tb_Merchants` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_renewalsrequireddocs`
--

LOCK TABLES `tb_renewalsrequireddocs` WRITE;
/*!40000 ALTER TABLE `tb_renewalsrequireddocs` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_renewalsrequireddocs` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-09-05 14:56:58
