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
-- Table structure for table `tb_users`
--

DROP TABLE IF EXISTS `tb_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_users` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` varchar(100) NOT NULL,
  `UserName` varchar(200) NOT NULL,
  `Password` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsReset` tinyint(1) NOT NULL,
  `IsLocked` tinyint(1) NOT NULL,
  `IsLogggedIN` tinyint(1) NOT NULL,
  `InsertDate` date NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `modifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  `LanguageID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_tb_Users_Gen_tb_Languages` (`LanguageID`),
  CONSTRAINT `FK_tb_Users_Gen_tb_Languages` FOREIGN KEY (`LanguageID`) REFERENCES `lkp_tb_languages` (`LanguageID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_users`
--

LOCK TABLES `tb_users` WRITE;
/*!40000 ALTER TABLE `tb_users` DISABLE KEYS */;
INSERT INTO `tb_users` VALUES (2,'admin','System Admin','124556',1,0,0,0,'2014-08-31',1,NULL,NULL,1),(3,'Salesrep','sales rep','1245',1,0,0,0,'2014-08-31',1,NULL,NULL,1),(4,'user1','User','125212',1,0,0,0,'2014-08-31',1,NULL,NULL,1),(5,'user2','User','125212',1,0,0,0,'2014-08-31',1,NULL,NULL,1),(6,'user3','User','125212',1,0,0,0,'2014-08-31',1,NULL,NULL,1),(7,'user4','User','125212',1,0,0,0,'2014-08-31',1,NULL,NULL,1),(8,'user5','User','125212',1,0,0,0,'2014-08-31',1,NULL,NULL,1);
/*!40000 ALTER TABLE `tb_users` ENABLE KEYS */;
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
