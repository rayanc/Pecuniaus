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
-- Table structure for table `lkp_tb_statuses`
--

DROP TABLE IF EXISTS `lkp_tb_statuses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_statuses` (
  `StatusId` int(11) NOT NULL,
  `StatusTypeId` varchar(5) DEFAULT NULL,
  `StatusName` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`StatusId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_statuses`
--

LOCK TABLES `lkp_tb_statuses` WRITE;
/*!40000 ALTER TABLE `lkp_tb_statuses` DISABLE KEYS */;
INSERT INTO `lkp_tb_statuses` VALUES (10000,'MRC','Inactive',1),(10001,'MRC','Contract being paid on time',1),(10002,'MRC','Contract being paid faster than expected',1),(10003,'MRC','Contract being paid slower than expected',1),(10004,'MRC','Investigation',1),(10005,'MRC','Collection',1),(20001,'CNT','Prequalification',1),(20002,'CNT','Document Gathering',1),(20003,'CNT','Processing Documents',1),(20004,'CNT','Review',1),(20005,'CNT','Contarct Approval',1),(20006,'CNT','Contract being Signed',1),(20007,'CNT','Funded',1),(20008,'CNT','Contracts Being Signed',1),(20009,'CNT','Written Off',1),(21001,'CNT','In Renewal Proccess',1),(21002,'CNT','In Review',1),(21003,'CNT','Renewal Waiting for Approval',1),(21004,'CNT','Contracts Being Signed',1);
/*!40000 ALTER TABLE `lkp_tb_statuses` ENABLE KEYS */;
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
