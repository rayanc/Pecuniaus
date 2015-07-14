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
-- Table structure for table `tb_tasks`
--

DROP TABLE IF EXISTS `tb_tasks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_tasks` (
  `TaskId` bigint(20) NOT NULL AUTO_INCREMENT,
  `TaskTypeId` int(11) NOT NULL,
  `StatusId` int(11) NOT NULL,
  `Priority` int(11) NOT NULL,
  `StartDate` datetime NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ContractId` bigint(20) NOT NULL,
  `AssignedUserId` bigint(20) DEFAULT NULL,
  `AssignedDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) NOT NULL,
  `ModifyDate` datetime NOT NULL,
  `WorkflowId` smallint(6) NOT NULL,
  `AffiliationFlag` tinyint(1) NOT NULL,
  `MerchantIDTMP` bigint(20) DEFAULT NULL,
  `enddate` datetime DEFAULT NULL,
  PRIMARY KEY (`TaskId`),
  KEY `FK_tb_Tasks_tb_Merchants` (`MerchantId`),
  KEY `FK_tb_Tasks_tb_Workflows` (`WorkflowId`),
  KEY `FK_tb_Tasks_TMP_tb_Merchants` (`MerchantIDTMP`),
  KEY `FK_tb_TaskTypes_TaskTypeId` (`TaskTypeId`),
  CONSTRAINT `FK_tb_TaskTypes_TaskTypeId` FOREIGN KEY (`TaskTypeId`) REFERENCES `tb_tasktypes` (`TaskTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Tasks_Gen_tb_Statuses` FOREIGN KEY (`MerchantIDTMP`) REFERENCES `tmp_tb_merchants` (`MerchantId_TMP`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Tasks_TMP_tb_Merchants` FOREIGN KEY (`MerchantIDTMP`) REFERENCES `tmp_tb_merchants` (`MerchantId_TMP`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Tasks_tb_Merchants` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Tasks_tb_Workflows` FOREIGN KEY (`WorkflowId`) REFERENCES `tb_workflows` (`WorkFlowId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_tasks`
--

LOCK TABLES `tb_tasks` WRITE;
/*!40000 ALTER TABLE `tb_tasks` DISABLE KEYS */;
INSERT INTO `tb_tasks` VALUES (12,1,1002,0,'2014-08-31 00:00:00',1,1,2,'2014-08-31 00:00:00',1,'2014-08-31 00:00:00',1,'2014-08-31 00:00:00',1,0,1,NULL),(13,2,1002,0,'2014-08-31 00:00:00',1,1,2,'2014-08-31 00:00:00',1,'2014-08-31 00:00:00',1,'2014-08-31 00:00:00',1,0,1,NULL);
/*!40000 ALTER TABLE `tb_tasks` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-09-05 14:56:55
