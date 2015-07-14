CREATE DATABASE  IF NOT EXISTS `portaldb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `portaldb`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: localhost    Database: portaldb
-- ------------------------------------------------------
-- Server version	5.6.20-log

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
-- Table structure for table `lkp_tb_addresstype`
--

DROP TABLE IF EXISTS `lkp_tb_addresstype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_addresstype` (
  `AddressTypeId` smallint(6) NOT NULL,
  `AddressType` varchar(50) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InnsertDate` datetime NOT NULL,
  PRIMARY KEY (`AddressTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_addresstype`
--

LOCK TABLES `lkp_tb_addresstype` WRITE;
/*!40000 ALTER TABLE `lkp_tb_addresstype` DISABLE KEYS */;
INSERT INTO `lkp_tb_addresstype` VALUES (1,'Physical Address',1,1,'2014-09-16 00:00:00'),(2,'Owner Address',1,1,'2014-09-16 00:00:00'),(3,'Contact Address',1,1,'2014-09-16 00:00:00');
/*!40000 ALTER TABLE `lkp_tb_addresstype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_declinereasons`
--

DROP TABLE IF EXISTS `lkp_tb_declinereasons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_declinereasons` (
  `DeclineReasonId` bigint(20) NOT NULL,
  `workFlowId` smallint(6) NOT NULL,
  `ReasonDescription` longtext NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`DeclineReasonId`),
  KEY `FK_tb_DeclineReasons_tb_Workflows` (`workFlowId`),
  CONSTRAINT `FK_tb_DeclineReasons_tb_Workflows` FOREIGN KEY (`workFlowId`) REFERENCES `tb_workflows` (`WorkFlowId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_declinereasons`
--

LOCK TABLES `lkp_tb_declinereasons` WRITE;
/*!40000 ALTER TABLE `lkp_tb_declinereasons` DISABLE KEYS */;
INSERT INTO `lkp_tb_declinereasons` VALUES (15001,1,'Unacceptable Credit Score – 90 days',1),(15002,1,'Merchant doesn’t fulfill Gross Yearly Sales requirement –90 days',1),(15003,1,'Merchant doesn’t have authorized Credit Card Processor – 120 days',1),(15004,1,'Merchant doesn’t fulfill minimum CC transactions requirement -',1),(15005,1,'CC sales have gone down – 90 days',1),(15006,1,'Less than 5 months accepting CC payments – 150 days',1),(15007,1,'CC volumes don’t fulfill requirements–120 days',1),(15008,1,'The person who signed the form is not an authorized person – 120 days',1),(15009,1,'The Merchant has already been created by another Rep',1),(15010,1,'Merchant stopped processing.',1),(15011,1,'The client doesn’t want a loan at this time',1),(15012,1,'The client declines because of the Administrative Expenses',1),(15013,1,'The client declines because of the price',1),(15014,1,'The client declines because of the Retention Percentage',1),(15015,1,'The client wants a bigger loan',1),(15016,1,'The client wants a higher repayment time',1),(15017,1,'The client is unreachable / refuses to return calls',1),(15018,1,'The client refuses to present required documents',1),(15019,1,'Merchant has different financing option',1);
/*!40000 ALTER TABLE `lkp_tb_declinereasons` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_documenttypes`
--

DROP TABLE IF EXISTS `lkp_tb_documenttypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_documenttypes` (
  `DocumentTypeId` int(11) NOT NULL,
  `Description` varchar(200) NOT NULL,
  `WorkFlowId` smallint(6) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `Modifydate` datetime DEFAULT NULL,
  PRIMARY KEY (`DocumentTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_documenttypes`
--

LOCK TABLES `lkp_tb_documenttypes` WRITE;
/*!40000 ALTER TABLE `lkp_tb_documenttypes` DISABLE KEYS */;
INSERT INTO `lkp_tb_documenttypes` VALUES (1,'Contract Application',1,1,1,'2014-09-16 00:00:00',NULL,NULL),(2,'Legal Documents of the Company ',2,1,1,'2014-09-16 00:00:00',NULL,NULL),(3,'Commercial Name Verification Screenshot ',2,1,1,'2014-09-16 00:00:00',NULL,NULL),(4,'RNC Screenshot',2,1,1,'2014-09-16 00:00:00',NULL,NULL),(5,'ID or Passport',2,1,1,'2014-09-16 00:00:00',NULL,NULL),(6,'Lease Contract or Land Title',2,1,1,'2014-09-16 00:00:00',NULL,NULL),(7,'Null Check',2,1,1,'2014-09-16 00:00:00',NULL,NULL),(8,'Bank Statements',2,1,1,'2014-09-16 00:00:00',NULL,NULL);
/*!40000 ALTER TABLE `lkp_tb_documenttypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_entitytypes`
--

DROP TABLE IF EXISTS `lkp_tb_entitytypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_entitytypes` (
  `EntitytypeID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(70) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  PRIMARY KEY (`EntitytypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=11008 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_entitytypes`
--

LOCK TABLES `lkp_tb_entitytypes` WRITE;
/*!40000 ALTER TABLE `lkp_tb_entitytypes` DISABLE KEYS */;
INSERT INTO `lkp_tb_entitytypes` VALUES (11001,'Persona Fisica',NULL,'',1,'2014-09-16'),(11002,'Sociedad en Nombre Colectivo',NULL,'',1,'2014-09-16'),(11003,'Sociedad en Comandita Simple',NULL,'',1,'2014-09-16'),(11004,'Sociedad en Comandita por Acciones',NULL,'',1,'2014-09-16'),(11005,'Sociedad de Responsabilidad Limitada (S.R.L.)',NULL,'',1,'2014-09-16'),(11006,'Empresa Individual de Responsabilidad Limitada (E.I.R.L.)',NULL,'',1,'2014-09-16'),(11007,'Sociedad Anónima (S.A.)',NULL,'',1,'2014-09-16');
/*!40000 ALTER TABLE `lkp_tb_entitytypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_expencetypes`
--

DROP TABLE IF EXISTS `lkp_tb_expencetypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_expencetypes` (
  `ExpenseTypeId` int(11) NOT NULL,
  `Description` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`ExpenseTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_expencetypes`
--

LOCK TABLES `lkp_tb_expencetypes` WRITE;
/*!40000 ALTER TABLE `lkp_tb_expencetypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `lkp_tb_expencetypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_expenses`
--

DROP TABLE IF EXISTS `lkp_tb_expenses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_expenses` (
  `ExpenceId` int(11) NOT NULL,
  `ExpenseTypeId` int(11) NOT NULL,
  `ExpenseAmount` double NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertDate` date NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `ModifyDate` date DEFAULT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ExpenceId`),
  KEY `FK_tb_Expenses_Gen_tb_ExpenceTypes` (`ExpenseTypeId`),
  CONSTRAINT `FK_tb_Expenses_Gen_tb_ExpenceTypes` FOREIGN KEY (`ExpenseTypeId`) REFERENCES `lkp_tb_expencetypes` (`ExpenseTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_expenses`
--

LOCK TABLES `lkp_tb_expenses` WRITE;
/*!40000 ALTER TABLE `lkp_tb_expenses` DISABLE KEYS */;
/*!40000 ALTER TABLE `lkp_tb_expenses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_languages`
--

DROP TABLE IF EXISTS `lkp_tb_languages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_languages` (
  `LanguageID` int(11) NOT NULL,
  `LanguageName` varchar(100) NOT NULL,
  `CharSet` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`LanguageID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_languages`
--

LOCK TABLES `lkp_tb_languages` WRITE;
/*!40000 ALTER TABLE `lkp_tb_languages` DISABLE KEYS */;
INSERT INTO `lkp_tb_languages` VALUES (1,'English','ANSI',1,'2014-08-31 00:00:00',1,NULL,NULL);
/*!40000 ALTER TABLE `lkp_tb_languages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_notetypes`
--

DROP TABLE IF EXISTS `lkp_tb_notetypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_notetypes` (
  `NoteTypeId` int(11) NOT NULL,
  `Description` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`NoteTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_notetypes`
--

LOCK TABLES `lkp_tb_notetypes` WRITE;
/*!40000 ALTER TABLE `lkp_tb_notetypes` DISABLE KEYS */;
INSERT INTO `lkp_tb_notetypes` VALUES (500001,'Prequal Notes',1,1,'2014-09-16 00:00:00',NULL,NULL),(510001,'Contract Notes',1,1,'2014-09-16 00:00:00',NULL,NULL),(520001,'Renewal Notes',1,1,'2014-09-16 00:00:00',NULL,NULL),(530001,'General Notes',1,1,'2014-09-16 00:00:00',NULL,NULL);
/*!40000 ALTER TABLE `lkp_tb_notetypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_processorlist`
--

DROP TABLE IF EXISTS `lkp_tb_processorlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_processorlist` (
  `ProcessorId` smallint(6) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`ProcessorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_processorlist`
--

LOCK TABLES `lkp_tb_processorlist` WRITE;
/*!40000 ALTER TABLE `lkp_tb_processorlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `lkp_tb_processorlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_statuses`
--

DROP TABLE IF EXISTS `lkp_tb_statuses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_statuses` (
  `StatusId` int(11) NOT NULL,
  `StatusTypeId` varchar(10) NOT NULL,
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
INSERT INTO `lkp_tb_statuses` VALUES (10001,'MRC','Inactive',1),(10002,'MRC','Contract being paid on time',1),(10003,'MRC','Contract being paid faster than expected',1),(10004,'MRC','Contract being paid slower than expected',1),(10005,'MRC','Investigation',1),(10006,'MRC','Collection',1),(20001,'CNT','Prequalification',1),(20002,'CNT','Document Gathering',1),(20003,'CNT','Processing Documents',1),(20004,'CNT','Review',1),(20005,'CNT','Contarct Approval',1),(20006,'CNT','Contract being Signed',1),(20007,'CNT','Funded',1),(20008,'CNT','Contracts Being Signed',1),(20009,'CNT','Written Off',1),(20101,'CNT','In Renewal Proccess',1),(20102,'CNT','In Review',1),(20103,'CNT','Renewal Waiting for Approval',1),(20104,'CNT','Contracts Being Signed',1),(22001,'TSK','Unassigned',1),(22002,'TSK','Assigned',1),(22003,'TSK','CompleteByUser',1),(22004,'TSK','Complete',1);
/*!40000 ALTER TABLE `lkp_tb_statuses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lkp_tb_zipcode`
--

DROP TABLE IF EXISTS `lkp_tb_zipcode`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lkp_tb_zipcode` (
  `ZipCodeId` int(11) NOT NULL,
  `Zicode` mediumint(9) DEFAULT NULL,
  `CountryId` int(11) NOT NULL,
  `StateId` int(11) NOT NULL,
  `CityId` int(11) NOT NULL,
  `TimeZone` int(11) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDat` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`ZipCodeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lkp_tb_zipcode`
--

LOCK TABLES `lkp_tb_zipcode` WRITE;
/*!40000 ALTER TABLE `lkp_tb_zipcode` DISABLE KEYS */;
/*!40000 ALTER TABLE `lkp_tb_zipcode` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_activitytype`
--

DROP TABLE IF EXISTS `tb_activitytype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_activitytype` (
  `ActivityTypeId` int(11) NOT NULL,
  `ActivityName` varchar(100) NOT NULL,
  `Description` longtext NOT NULL,
  `IsActive` bigint(20) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `modifyUserId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ActivityTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_activitytype`
--

LOCK TABLES `tb_activitytype` WRITE;
/*!40000 ALTER TABLE `tb_activitytype` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_activitytype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_addresses`
--

DROP TABLE IF EXISTS `tb_addresses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_addresses` (
  `AddressId` bigint(20) NOT NULL AUTO_INCREMENT,
  `AddressTypeId` smallint(6) NOT NULL,
  `Address1` varchar(200) NOT NULL,
  `Address2` varchar(200) DEFAULT NULL,
  `Country` varchar(50) NOT NULL,
  `City` varchar(50) NOT NULL,
  `State` varchar(50) NOT NULL,
  `ZipCodeId` int(11) NOT NULL,
  `Phone1` varchar(12) DEFAULT NULL,
  `Phone2` varchar(12) DEFAULT NULL,
  `Phone3` varchar(12) DEFAULT NULL,
  `email1` varchar(50) DEFAULT NULL,
  `email2` varchar(50) DEFAULT NULL,
  `email3` varchar(50) DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`AddressId`),
  KEY `FK_Gen_tb_Addresses_Gen_tb_AddressType` (`AddressTypeId`),
  KEY `FK_Gen_tb_Addresses_tb_ZipCode` (`ZipCodeId`),
  CONSTRAINT `FK_Gen_tb_Addresses_Gen_tb_AddressType` FOREIGN KEY (`AddressTypeId`) REFERENCES `lkp_tb_addresstype` (`AddressTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Gen_tb_Addresses_tb_ZipCode` FOREIGN KEY (`ZipCodeId`) REFERENCES `lkp_tb_zipcode` (`ZipCodeId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_addresses`
--

LOCK TABLES `tb_addresses` WRITE;
/*!40000 ALTER TABLE `tb_addresses` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_addresses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_assignedcollections`
--

DROP TABLE IF EXISTS `tb_assignedcollections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_assignedcollections` (
  `AssignmentID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CollectorID` int(11) NOT NULL,
  `CollectorTypeID` smallint(6) NOT NULL,
  `OwnedBalance` double DEFAULT NULL,
  `AssignedByID` bigint(20) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`AssignmentID`),
  KEY `FK_tb_AssignedCollections_tb_Collectors` (`CollectorID`),
  KEY `FK_tb_AssignedCollections_tb_Users` (`AssignedByID`),
  CONSTRAINT `FK_tb_AssignedCollections_tb_Collectors` FOREIGN KEY (`CollectorID`) REFERENCES `tb_collectors` (`CollectorID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_AssignedCollections_tb_Users` FOREIGN KEY (`AssignedByID`) REFERENCES `tb_users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_assignedcollections`
--

LOCK TABLES `tb_assignedcollections` WRITE;
/*!40000 ALTER TABLE `tb_assignedcollections` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_assignedcollections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_collectionactivities`
--

DROP TABLE IF EXISTS `tb_collectionactivities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_collectionactivities` (
  `ActivityID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CollectionID` bigint(20) NOT NULL,
  `CollectionTypeID` smallint(6) NOT NULL,
  `ActivityTypeID` int(11) NOT NULL,
  `CollectionComments` varchar(100) DEFAULT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ActivityID`),
  KEY `FK_tb_CollectionActivities_tb_CollectionActivityList` (`ActivityTypeID`),
  KEY `FK_tb_CollectionActivities_tb_Collections` (`CollectionID`),
  CONSTRAINT `FK_tb_CollectionActivities_tb_CollectionActivityList` FOREIGN KEY (`ActivityTypeID`) REFERENCES `tb_collectionactivitylist` (`ActivityTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_CollectionActivities_tb_Collections` FOREIGN KEY (`CollectionID`) REFERENCES `tb_collections` (`CollectionID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_collectionactivities`
--

LOCK TABLES `tb_collectionactivities` WRITE;
/*!40000 ALTER TABLE `tb_collectionactivities` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_collectionactivities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_collectionactivitylist`
--

DROP TABLE IF EXISTS `tb_collectionactivitylist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_collectionactivitylist` (
  `ActivityTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ActivityTypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_collectionactivitylist`
--

LOCK TABLES `tb_collectionactivitylist` WRITE;
/*!40000 ALTER TABLE `tb_collectionactivitylist` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_collectionactivitylist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_collectioncontracts`
--

DROP TABLE IF EXISTS `tb_collectioncontracts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_collectioncontracts` (
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `OwnedAmount` double NOT NULL,
  `RepaymentAmount` double NOT NULL,
  `ExpectedTurn` double NOT NULL,
  `RealTurn` double NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` bigint(20) DEFAULT NULL,
  KEY `FK_tb_CollectionContracts_tb_Contracts` (`ContractID`),
  KEY `FK_tb_CollectionContracts_tb_Merchants` (`MerchantID`),
  CONSTRAINT `FK_Merchant_tb_collectioncontracts_MID` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_CollectionContracts_tb_Contracts` FOREIGN KEY (`ContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_collectioncontracts`
--

LOCK TABLES `tb_collectioncontracts` WRITE;
/*!40000 ALTER TABLE `tb_collectioncontracts` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_collectioncontracts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_collections`
--

DROP TABLE IF EXISTS `tb_collections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_collections` (
  `CollectionID` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `CollectionTypeID` smallint(6) NOT NULL,
  `StartDate` datetime NOT NULL,
  `AssignedDate` datetime NOT NULL,
  `AssignedUserID` bigint(20) DEFAULT NULL,
  `AssignedByUserID` bigint(20) DEFAULT NULL,
  `ClosedDate` datetime DEFAULT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `Comments` varchar(100) DEFAULT NULL,
  `StatusID` int(11) NOT NULL,
  `DefaultedDate` datetime NOT NULL,
  `SendLetterFlag` tinyint(1) NOT NULL,
  `CompanyID` smallint(6) DEFAULT NULL,
  `IsSpecialCase` tinyint(1) NOT NULL,
  PRIMARY KEY (`CollectionID`),
  KEY `FK_tb_Collections_tb_Contracts` (`ContractID`),
  KEY `FK_tb_Collections_tb_Merchants` (`MerchantID`),
  CONSTRAINT `FK_Merchant_tb_collections_MID` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Collections_tb_Contracts` FOREIGN KEY (`ContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_collections`
--

LOCK TABLES `tb_collections` WRITE;
/*!40000 ALTER TABLE `tb_collections` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_collections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_collectors`
--

DROP TABLE IF EXISTS `tb_collectors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_collectors` (
  `CollectorID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` bigint(20) NOT NULL,
  `CompanyID` smallint(6) NOT NULL,
  `ContactID` bigint(20) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDaet` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  PRIMARY KEY (`CollectorID`),
  KEY `FK_tb_Collectors_tb_Users` (`UserID`),
  KEY `FK_tb_Collectors_tb_Contacts` (`ContactID`),
  CONSTRAINT `FK_tb_CollectorContact` FOREIGN KEY (`ContactID`) REFERENCES `tb_contacts` (`ContactId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Collectors_tb_Users` FOREIGN KEY (`UserID`) REFERENCES `tb_users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_collectors`
--

LOCK TABLES `tb_collectors` WRITE;
/*!40000 ALTER TABLE `tb_collectors` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_collectors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_collectoruploadeddocuments`
--

DROP TABLE IF EXISTS `tb_collectoruploadeddocuments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_collectoruploadeddocuments` (
  `CollectionDOCID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CollectionID` bigint(20) NOT NULL,
  `CollectorID` int(11) NOT NULL,
  `DocumentTypeID` int(11) NOT NULL,
  `UploadDate` datetime NOT NULL,
  `DateAssignedToCollector` datetime NOT NULL,
  `InsetUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`CollectionDOCID`),
  KEY `FK_tb_CollectorUploadedDocuments_Gen_tb_DocumentTypes` (`DocumentTypeID`),
  KEY `FK_tb_CollectorUploadedDocuments_tb_Collections` (`CollectionID`),
  CONSTRAINT `FK_tb_CollectorUploadedDocuments_Gen_tb_DocumentTypes` FOREIGN KEY (`DocumentTypeID`) REFERENCES `lkp_tb_documenttypes` (`DocumentTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_CollectorUploadedDocuments_tb_Collections` FOREIGN KEY (`CollectionID`) REFERENCES `tb_collections` (`CollectionID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_collectoruploadeddocuments`
--

LOCK TABLES `tb_collectoruploadeddocuments` WRITE;
/*!40000 ALTER TABLE `tb_collectoruploadeddocuments` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_collectoruploadeddocuments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_contacts`
--

DROP TABLE IF EXISTS `tb_contacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_contacts` (
  `ContactId` bigint(20) NOT NULL AUTO_INCREMENT,
  `ContactTypeId` int(11) NOT NULL,
  `JobTitle` varchar(50) NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) NOT NULL,
  `AddressId1` bigint(20) DEFAULT NULL,
  `DateOfbirth` date NOT NULL,
  `SSN` varchar(50) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  PRIMARY KEY (`ContactId`),
  KEY `FK_tb_Contacts_Gen_tb_Addresses` (`AddressId1`),
  KEY `FK_Merchant_tb_contacts_MID` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_contacts_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Contacts_Gen_tb_Addresses` FOREIGN KEY (`AddressId1`) REFERENCES `tb_addresses` (`AddressId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_contacts`
--

LOCK TABLES `tb_contacts` WRITE;
/*!40000 ALTER TABLE `tb_contacts` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_contacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_contracts`
--

DROP TABLE IF EXISTS `tb_contracts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_contracts` (
  `ContractId` bigint(20) NOT NULL,
  `ContractNumber` varchar(50) NOT NULL,
  `CreationDate` datetime NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `StatusId` int(11) NOT NULL,
  `currencyId` int(11) NOT NULL,
  `LoanedAmount` double DEFAULT NULL,
  `OwnedAmount` double DEFAULT NULL,
  `PaidAmount` double DEFAULT NULL,
  `Turn` int(11) DEFAULT NULL,
  `fundingDate` datetime DEFAULT NULL,
  `LastPaymentDate` datetime DEFAULT NULL,
  `StatusModifiedDate` datetime DEFAULT NULL,
  `LoanAppliedDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `ContractTypeId` int(11) NOT NULL,
  `OwnedBalanceZeroDate` datetime DEFAULT NULL,
  `LoanedBalanceZeroDate` datetime DEFAULT NULL,
  `WriteOffBalanceZeroDate` datetime DEFAULT NULL,
  `WrittenOffReasonId` int(11) DEFAULT NULL,
  `DafaultedDate` datetime DEFAULT NULL,
  `BuyRate` double DEFAULT NULL,
  `DailyPayment` double DEFAULT NULL,
  `AdjustedTurn` int(11) DEFAULT NULL,
  `RenewedID` bigint(20) DEFAULT NULL,
  `RenewalID` bigint(20) DEFAULT NULL,
  `RenewalContractID` bigint(20) DEFAULT NULL,
  `IsSpecialCase` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`ContractId`),
  KEY `FK_tb_Contracts_Gen_tb_Statuses` (`StatusId`),
  KEY `FK_tb_Contracts_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_contracts_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Contracts_Gen_tb_Statuses` FOREIGN KEY (`StatusId`) REFERENCES `lkp_tb_statuses` (`StatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_contracts`
--

LOCK TABLES `tb_contracts` WRITE;
/*!40000 ALTER TABLE `tb_contracts` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_contracts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_creditcardactivity`
--

DROP TABLE IF EXISTS `tb_creditcardactivity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_creditcardactivity` (
  `CreditCardActivityId` bigint(20) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ProcessorId` int(11) NOT NULL,
  `ProcessorMerchantId` bigint(20) NOT NULL,
  `AcctivityTypeId` int(11) NOT NULL,
  `CurrencyId` int(11) NOT NULL,
  `TotalAmount` double NOT NULL,
  `TotalTickets` int(11) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `ProcessedDate` datetime NOT NULL,
  `IndustryId` int(11) NOT NULL,
  `MerchantStatusId` int(11) NOT NULL,
  `AuthorizedOwnerId` bigint(20) NOT NULL,
  PRIMARY KEY (`CreditCardActivityId`),
  KEY `FK_tb_CreditCardActivity_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_creditcardactivity_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_creditcardactivity`
--

LOCK TABLES `tb_creditcardactivity` WRITE;
/*!40000 ALTER TABLE `tb_creditcardactivity` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_creditcardactivity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_creditcardprofiles`
--

DROP TABLE IF EXISTS `tb_creditcardprofiles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_creditcardprofiles` (
  `CreditCardProfileId` bigint(20) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ProcessorId` bigint(20) NOT NULL,
  PRIMARY KEY (`CreditCardProfileId`),
  KEY `FK_tb_CreditCardProfiles_tb_Merchants` (`MerchantId`),
  KEY `FK_tb_CreditCardProfiles_tb_Processor` (`ProcessorId`),
  CONSTRAINT `FK_Merchant_tb_creditcardprofiles_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_CreditCardProfiles_tb_Processor` FOREIGN KEY (`ProcessorId`) REFERENCES `tb_processor` (`ProcessorId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_creditcardprofiles`
--

LOCK TABLES `tb_creditcardprofiles` WRITE;
/*!40000 ALTER TABLE `tb_creditcardprofiles` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_creditcardprofiles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_creditreports`
--

DROP TABLE IF EXISTS `tb_creditreports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_creditreports` (
  `CreditReportID` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `Image` longblob NOT NULL,
  `CreditScore` int(11) NOT NULL,
  `NumberofCreditCards` smallint(6) NOT NULL,
  `CreditAvailable` int(11) NOT NULL,
  `TimeofReport` datetime NOT NULL,
  `OtherCredits` tinyint(1) NOT NULL,
  `FirstName` varchar(100) DEFAULT NULL,
  `Name` varchar(200) DEFAULT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) DEFAULT NULL,
  `Passport` varchar(50) DEFAULT NULL,
  `Occupation` varchar(50) DEFAULT NULL,
  `Nationality` varchar(50) DEFAULT NULL,
  `RawData` longblob,
  `Errors` varchar(200) DEFAULT NULL,
  `RiskCategory` varchar(50) DEFAULT NULL,
  `ProbabilityOfDefault` double DEFAULT NULL,
  `MonthEvaualted` date DEFAULT NULL,
  PRIMARY KEY (`CreditReportID`),
  KEY `FK_tb_CreditReports_tb_Merchants` (`MerchantID`),
  CONSTRAINT `FK_Merchant_tb_creditreports_MID` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_creditreports`
--

LOCK TABLES `tb_creditreports` WRITE;
/*!40000 ALTER TABLE `tb_creditreports` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_creditreports` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_documents`
--

DROP TABLE IF EXISTS `tb_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_documents` (
  `DocumentId` bigint(20) NOT NULL AUTO_INCREMENT,
  `DocumentTypeId` int(11) NOT NULL,
  `FileName` varchar(1000) NOT NULL,
  `FileDetails` longblob NOT NULL,
  `MERCHANTID` bigint(20) NOT NULL,
  `UploaduserId` bigint(20) NOT NULL,
  `UploadDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`DocumentId`),
  KEY `FK__tb_Docume__Docum__0D7A0286` (`DocumentTypeId`),
  KEY `FK_Merchant_tb_documents_MID` (`MERCHANTID`),
  CONSTRAINT `FK_Merchant_tb_documents_MID` FOREIGN KEY (`MERCHANTID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK__tb_Docume__Docum__0C85DE4D` FOREIGN KEY (`DocumentTypeId`) REFERENCES `lkp_tb_documenttypes` (`DocumentTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK__tb_Docume__Docum__0D7A0286` FOREIGN KEY (`DocumentTypeId`) REFERENCES `lkp_tb_documenttypes` (`DocumentTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_documents`
--

LOCK TABLES `tb_documents` WRITE;
/*!40000 ALTER TABLE `tb_documents` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_documents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_emailgroup`
--

DROP TABLE IF EXISTS `tb_emailgroup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_emailgroup` (
  `GroupId` int(11) NOT NULL,
  `GroupName` varchar(50) NOT NULL,
  `IsActive` bigint(20) NOT NULL,
  PRIMARY KEY (`GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_emailgroup`
--

LOCK TABLES `tb_emailgroup` WRITE;
/*!40000 ALTER TABLE `tb_emailgroup` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_emailgroup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_emailgroupmembers`
--

DROP TABLE IF EXISTS `tb_emailgroupmembers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_emailgroupmembers` (
  `GroupMemberId` bigint(20) NOT NULL,
  `GroupId` int(11) NOT NULL,
  `MemberEmail` varchar(1000) NOT NULL,
  `IsActive` bigint(20) NOT NULL,
  PRIMARY KEY (`GroupMemberId`),
  KEY `FK_tb_EmailGroupMembers_tb_EmailGroup` (`GroupId`),
  CONSTRAINT `FK_tb_EmailGroupMembers_tb_EmailGroup` FOREIGN KEY (`GroupId`) REFERENCES `tb_emailgroup` (`GroupId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_emailgroupmembers`
--

LOCK TABLES `tb_emailgroupmembers` WRITE;
/*!40000 ALTER TABLE `tb_emailgroupmembers` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_emailgroupmembers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_grouprights`
--

DROP TABLE IF EXISTS `tb_grouprights`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_grouprights` (
  `GroupRightID` bigint(20) NOT NULL AUTO_INCREMENT,
  `GroupID` int(11) NOT NULL,
  `ModuleID` int(11) NOT NULL,
  `Read` tinyint(1) NOT NULL,
  `Write` tinyint(1) NOT NULL,
  `Edit` tinyint(1) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  PRIMARY KEY (`GroupRightID`),
  KEY `FK_tb_GroupRights_tb_Groups` (`GroupID`),
  KEY `FK_tb_GroupRights_tb_Users` (`InsertUserID`),
  CONSTRAINT `FK_tb_GroupRights_tb_Groups` FOREIGN KEY (`GroupID`) REFERENCES `tb_groups` (`GroupID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_GroupRights_tb_Users` FOREIGN KEY (`InsertUserID`) REFERENCES `tb_users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_grouprights`
--

LOCK TABLES `tb_grouprights` WRITE;
/*!40000 ALTER TABLE `tb_grouprights` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_grouprights` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_groups`
--

DROP TABLE IF EXISTS `tb_groups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_groups` (
  `GroupID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupName` varchar(100) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  PRIMARY KEY (`GroupID`),
  KEY `FK_tb_Groups_tb_Users` (`InsertUserID`),
  CONSTRAINT `FK_tb_Groups_tb_Users` FOREIGN KEY (`InsertUserID`) REFERENCES `tb_users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_groups`
--

LOCK TABLES `tb_groups` WRITE;
/*!40000 ALTER TABLE `tb_groups` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_groups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_merchantactivities`
--

DROP TABLE IF EXISTS `tb_merchantactivities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_merchantactivities` (
  `ActivityId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `ActivityTypeId` int(11) NOT NULL,
  PRIMARY KEY (`ActivityId`),
  KEY `FK_tb_MerchantActivities_tb_ActivityType` (`ActivityTypeId`),
  KEY `FK_tb_MerchantActivities_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_merchantactivities_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_MerchantActivities_tb_ActivityType` FOREIGN KEY (`ActivityTypeId`) REFERENCES `tb_activitytype` (`ActivityTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_merchantactivities`
--

LOCK TABLES `tb_merchantactivities` WRITE;
/*!40000 ALTER TABLE `tb_merchantactivities` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_merchantactivities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_merchantbankstatements`
--

DROP TABLE IF EXISTS `tb_merchantbankstatements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_merchantbankstatements` (
  `BankInfoId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) NOT NULL,
  `StatementId` bigint(20) NOT NULL,
  `Month` smallint(6) NOT NULL,
  `Year` smallint(6) NOT NULL,
  `amount` double NOT NULL,
  PRIMARY KEY (`BankInfoId`),
  KEY `FK_tb_MerchantBankStatements_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_merchantbankstatements_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_merchantbankstatements`
--

LOCK TABLES `tb_merchantbankstatements` WRITE;
/*!40000 ALTER TABLE `tb_merchantbankstatements` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_merchantbankstatements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_merchantlandlords`
--

DROP TABLE IF EXISTS `tb_merchantlandlords`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_merchantlandlords` (
  `LandlordId` int(11) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `BuildingName` varchar(100) NOT NULL,
  `ContactId` bigint(20) NOT NULL,
  PRIMARY KEY (`LandlordId`),
  KEY `FK_tb_MerchantLandlords_tb_Contacts` (`ContactId`),
  KEY `FK_tb_MerchantLandlords_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_merchantlandlords_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_LandlordContact` FOREIGN KEY (`ContactId`) REFERENCES `tb_contacts` (`ContactId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_merchantlandlords`
--

LOCK TABLES `tb_merchantlandlords` WRITE;
/*!40000 ALTER TABLE `tb_merchantlandlords` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_merchantlandlords` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_merchants`
--

DROP TABLE IF EXISTS `tb_merchants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_merchants` (
  `MerchantId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyId` smallint(6) NOT NULL,
  `LegalName` varchar(200) NOT NULL,
  `BusinessName` varchar(200) NOT NULL,
  `WorkflowId` smallint(6) NOT NULL,
  `AddressId` bigint(20) DEFAULT NULL,
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
  `RentFlag` tinyint(1) DEFAULT NULL,
  `LandlordName` varchar(100) DEFAULT NULL,
  `RentedAmount` double DEFAULT NULL,
  `currencyId` int(11) DEFAULT NULL,
  `RNCNumber` varchar(11) NOT NULL,
  `TelePhone1` varchar(12) DEFAULT NULL,
  `TelePhone2` varchar(12) DEFAULT NULL,
  PRIMARY KEY (`MerchantId`),
  KEY `FK_tb_Merchants_Gen_tb_Addresses` (`AddressId`),
  KEY `FK_tb_Merchants_Gen_tb_Addresses1` (`AddressId2`),
  KEY `FK_tb_Merchants_Gen_tb_Statuses` (`StatusId`),
  KEY `FK_tb_Merchants_tb_SalesRep` (`SalesRepId`),
  CONSTRAINT `FK_tb_Merchants_Gen_tb_Addresses` FOREIGN KEY (`AddressId`) REFERENCES `tb_addresses` (`AddressId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Merchants_Gen_tb_Addresses1` FOREIGN KEY (`AddressId2`) REFERENCES `tb_addresses` (`AddressId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Merchants_Gen_tb_Statuses` FOREIGN KEY (`StatusId`) REFERENCES `lkp_tb_statuses` (`StatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Merchants_tb_SalesRep` FOREIGN KEY (`SalesRepId`) REFERENCES `tb_salesrep` (`SalesRepId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_merchants`
--

LOCK TABLES `tb_merchants` WRITE;
/*!40000 ALTER TABLE `tb_merchants` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_merchants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_merchanttradereference`
--

DROP TABLE IF EXISTS `tb_merchanttradereference`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_merchanttradereference` (
  `TradeReferenceId` bigint(20) NOT NULL AUTO_INCREMENT,
  `TradeReferenceName` varchar(200) NOT NULL,
  `TelePhone` varchar(12) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `MerchantId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`TradeReferenceId`),
  KEY `FK_MerchantID_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_merchanttradereference_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_merchanttradereference`
--

LOCK TABLES `tb_merchanttradereference` WRITE;
/*!40000 ALTER TABLE `tb_merchanttradereference` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_merchanttradereference` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_notes`
--

DROP TABLE IF EXISTS `tb_notes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_notes` (
  `NoteId` bigint(20) NOT NULL,
  `NoteTypeId` int(11) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ContractId` bigint(20) NOT NULL,
  `Note` longtext NOT NULL,
  `WorkFlowId` smallint(6) NOT NULL,
  `ScreenName` varchar(50) NOT NULL,
  PRIMARY KEY (`NoteId`),
  KEY `FK_tb_Notes_Gen_tb_NoteTypes` (`NoteTypeId`),
  KEY `FK_tb_Notes_tb_Contracts` (`ContractId`),
  KEY `FK_tb_Notes_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_notes_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Notes_Gen_tb_NoteTypes` FOREIGN KEY (`NoteTypeId`) REFERENCES `lkp_tb_notetypes` (`NoteTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Notes_tb_Contracts` FOREIGN KEY (`ContractId`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_notes`
--

LOCK TABLES `tb_notes` WRITE;
/*!40000 ALTER TABLE `tb_notes` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_notes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_offers`
--

DROP TABLE IF EXISTS `tb_offers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_offers` (
  `OfferId` bigint(20) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ContractId` bigint(20) NOT NULL,
  `Turn` int(11) NOT NULL,
  `LoanAmount` double NOT NULL,
  `OwnedAmount` double NOT NULL,
  `Proportion` double NOT NULL,
  `Retention` double NOT NULL,
  `OfferCreationDate` datetime NOT NULL,
  `OfferAcceptanceDate` datetime NOT NULL,
  `CreatedUserId` bigint(20) NOT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ExpirationDate` datetime NOT NULL,
  PRIMARY KEY (`OfferId`),
  KEY `FK_tb_Offers_tb_Contracts` (`ContractId`),
  KEY `FK_tb_Offers_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_offers_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Offers_tb_Contracts` FOREIGN KEY (`ContractId`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_offers`
--

LOCK TABLES `tb_offers` WRITE;
/*!40000 ALTER TABLE `tb_offers` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_offers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_owners`
--

DROP TABLE IF EXISTS `tb_owners`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_owners` (
  `OwnerId` bigint(20) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `BusinessStartDate` datetime NOT NULL,
  `RentFlag` tinyint(1) NOT NULL,
  `RentAmount` double NOT NULL,
  `MortgageAmount` double DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUser` datetime NOT NULL,
  `ModifyDate` datetime NOT NULL,
  `ContactId` bigint(20) NOT NULL,
  PRIMARY KEY (`OwnerId`),
  KEY `FK_tb_Owners_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_owners_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_owners`
--

LOCK TABLES `tb_owners` WRITE;
/*!40000 ALTER TABLE `tb_owners` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_owners` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_prequalusers`
--

DROP TABLE IF EXISTS `tb_prequalusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_prequalusers` (
  `UserID` bigint(20) DEFAULT NULL,
  `TaskTypeID` int(11) DEFAULT NULL,
  `AssignedDate` datetime DEFAULT NULL,
  `WorkFlowID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_prequalusers`
--

LOCK TABLES `tb_prequalusers` WRITE;
/*!40000 ALTER TABLE `tb_prequalusers` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_prequalusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_processor`
--

DROP TABLE IF EXISTS `tb_processor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_processor` (
  `ProcessorId` bigint(20) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ProcessorTypeIds` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`ProcessorId`),
  KEY `FK_tb_Processor_Gen_tb_ProcessorList` (`ProcessorTypeIds`),
  KEY `FK_tb_Processor_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_processor_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Processor_Gen_tb_ProcessorList` FOREIGN KEY (`ProcessorTypeIds`) REFERENCES `lkp_tb_processorlist` (`ProcessorId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_processor`
--

LOCK TABLES `tb_processor` WRITE;
/*!40000 ALTER TABLE `tb_processor` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_processor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_processoractivities`
--

DROP TABLE IF EXISTS `tb_processoractivities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_processoractivities` (
  `ActivityId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) NOT NULL,
  `ProcessorId` smallint(6) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `TotalAmount` double NOT NULL,
  `AppliedAmount` double NOT NULL,
  `BalanceAmount` double NOT NULL,
  `PaidAmount` double NOT NULL,
  `ActivityTypeId` int(11) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) NOT NULL,
  `ModifyDate` datetime NOT NULL,
  PRIMARY KEY (`ActivityId`),
  KEY `FK_tb_ProcessorActivities_Gen_tb_ProcessorList` (`ProcessorId`),
  KEY `FK_tb_ProcessorActivities_tb_ActivityType` (`ActivityTypeId`),
  KEY `FK_tb_ProcessorActivities_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tb_processoractivities_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_ProcessorActivities_Gen_tb_ProcessorList` FOREIGN KEY (`ProcessorId`) REFERENCES `lkp_tb_processorlist` (`ProcessorId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_ProcessorActivities_tb_ActivityType` FOREIGN KEY (`ActivityTypeId`) REFERENCES `tb_activitytype` (`ActivityTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_processoractivities`
--

LOCK TABLES `tb_processoractivities` WRITE;
/*!40000 ALTER TABLE `tb_processoractivities` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_processoractivities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_renewals`
--

DROP TABLE IF EXISTS `tb_renewals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_renewals` (
  `RenewalID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `MerchantID` bigint(20) NOT NULL,
  `RenewedContractID` bigint(20) NOT NULL,
  `SalesRepID` bigint(20) NOT NULL,
  `RenewalAssignedUSerID` bigint(20) NOT NULL,
  `StatusID` int(11) DEFAULT NULL,
  `DeclineReasonID` int(11) DEFAULT NULL,
  `DeclineReasonComment` varchar(200) DEFAULT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  PRIMARY KEY (`RenewalID`),
  KEY `FK_tb_Renewals_tb_Contracts` (`ContractID`),
  KEY `FK_tb_Renewals_tb_Contracts1` (`RenewedContractID`),
  KEY `FK_tb_Renewals_tb_Merchants` (`MerchantID`),
  CONSTRAINT `FK_Merchant_tb_renewals_MID` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Renewals_tb_Contracts` FOREIGN KEY (`ContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Renewals_tb_Contracts1` FOREIGN KEY (`RenewedContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Renewals_tb_RenewalsList` FOREIGN KEY (`RenewalID`) REFERENCES `tb_renewalslist` (`RenewalID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_renewals`
--

LOCK TABLES `tb_renewals` WRITE;
/*!40000 ALTER TABLE `tb_renewals` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_renewals` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_renewalslist`
--

DROP TABLE IF EXISTS `tb_renewalslist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_renewalslist` (
  `RenewalID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ContractID` bigint(20) NOT NULL,
  `MerchantID` bigint(20) DEFAULT NULL,
  `StatusID` int(11) NOT NULL,
  `RenewalEligibleDate` date NOT NULL,
  `AssignedUserID` bigint(20) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  `CompanyID` smallint(6) NOT NULL,
  `PostponedTilldate` date DEFAULT NULL,
  PRIMARY KEY (`RenewalID`),
  KEY `FK_tb_RenewalsList_Gen_tb_Statuses` (`StatusID`),
  KEY `FK_tb_RenewalsList_tb_Contracts` (`ContractID`),
  KEY `FK_tb_RenewalsList_tb_Merchants` (`MerchantID`),
  CONSTRAINT `FK_Merchant_tb_renewalslist_MID` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_RenewalsList_Gen_tb_Statuses` FOREIGN KEY (`StatusID`) REFERENCES `lkp_tb_statuses` (`StatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_RenewalsList_tb_Contracts` FOREIGN KEY (`ContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_renewalslist`
--

LOCK TABLES `tb_renewalslist` WRITE;
/*!40000 ALTER TABLE `tb_renewalslist` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_renewalslist` ENABLE KEYS */;
UNLOCK TABLES;

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
  CONSTRAINT `FK_Merchant_tb_renewalsrequireddocs_MID` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_RenewalsRequiredDocs_Gen_tb_DocumentTypes` FOREIGN KEY (`RequiredDocTypeID`) REFERENCES `lkp_tb_documenttypes` (`DocumentTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_RenewalsRequiredDocs_tb_Contracts` FOREIGN KEY (`ContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_renewalsrequireddocs`
--

LOCK TABLES `tb_renewalsrequireddocs` WRITE;
/*!40000 ALTER TABLE `tb_renewalsrequireddocs` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_renewalsrequireddocs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_repaymentplan`
--

DROP TABLE IF EXISTS `tb_repaymentplan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_repaymentplan` (
  `RepaymentPlanID` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `CompanyID` smallint(6) NOT NULL,
  `DueDate` datetime NOT NULL,
  `Amount` double NOT NULL,
  `CollectedAmount` double NOT NULL,
  `CollectionDate` datetime NOT NULL,
  `CollectionFrequency` smallint(6) NOT NULL,
  `StatusID` int(11) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`RepaymentPlanID`),
  KEY `FK_tb_RepaymentPlan_tb_Contracts` (`ContractID`),
  KEY `FK_tb_RepaymentPlan_tb_Merchants` (`MerchantID`),
  CONSTRAINT `FK_Merchant_tb_repaymentplan_MID` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_RepaymentPlan_tb_Contracts` FOREIGN KEY (`ContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_repaymentplan`
--

LOCK TABLES `tb_repaymentplan` WRITE;
/*!40000 ALTER TABLE `tb_repaymentplan` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_repaymentplan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_salesrep`
--

DROP TABLE IF EXISTS `tb_salesrep`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_salesrep` (
  `SalesRepId` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserId` bigint(20) NOT NULL,
  `CompanyId` smallint(6) NOT NULL,
  `Insertdate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) DEFAULT NULL,
  `UpdateUserId` bigint(20) DEFAULT NULL,
  `Modifieddate` datetime DEFAULT NULL,
  `IsManager` tinyint(1) DEFAULT NULL,
  `AddressId` bigint(20) DEFAULT NULL,
  `Contactid` bigint(20) NOT NULL,
  `Commission` double NOT NULL,
  `FirstFundingDate` datetime DEFAULT NULL,
  PRIMARY KEY (`SalesRepId`),
  KEY `FK_tb_SalesRep_tb_SalesRep_Contact` (`Contactid`),
  CONSTRAINT `FK_tb_SalesRep_tb_SalesRep_Contact` FOREIGN KEY (`Contactid`) REFERENCES `tb_salesrep_contact` (`ContactId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_salesrep`
--

LOCK TABLES `tb_salesrep` WRITE;
/*!40000 ALTER TABLE `tb_salesrep` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_salesrep` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_salesrep_contact`
--

DROP TABLE IF EXISTS `tb_salesrep_contact`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_salesrep_contact` (
  `ContactId` bigint(20) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `SSN` varchar(9) DEFAULT NULL,
  `Jobtitle` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ContactId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_salesrep_contact`
--

LOCK TABLES `tb_salesrep_contact` WRITE;
/*!40000 ALTER TABLE `tb_salesrep_contact` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_salesrep_contact` ENABLE KEYS */;
UNLOCK TABLES;

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
  `EndDatte` datetime NOT NULL,
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
  PRIMARY KEY (`TaskId`),
  KEY `FK_tb_Tasks_tb_Merchants` (`MerchantId`),
  KEY `FK_tb_Tasks_tb_Workflows` (`WorkflowId`),
  KEY `FK_tb_Tasks_TMP_tb_Merchants` (`MerchantIDTMP`),
  KEY `FK_tb_TaskTypes_TaskTypeId` (`TaskTypeId`),
  KEY `FK_tb_Tasks_Gen_tb_Statuses_idx` (`StatusId`),
  CONSTRAINT `FK_Merchant_tb_tasks_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_TaskTypes_TaskTypeId` FOREIGN KEY (`TaskTypeId`) REFERENCES `tb_tasktypes` (`TaskTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Tasks_Gen_tb_Statuses` FOREIGN KEY (`StatusId`) REFERENCES `lkp_tb_statuses` (`StatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Tasks_tb_Workflows` FOREIGN KEY (`WorkflowId`) REFERENCES `tb_workflows` (`WorkFlowId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_tasks`
--

LOCK TABLES `tb_tasks` WRITE;
/*!40000 ALTER TABLE `tb_tasks` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_tasks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_tasktypes`
--

DROP TABLE IF EXISTS `tb_tasktypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_tasktypes` (
  `WorkflowId` smallint(6) NOT NULL,
  `TaskTypeId` int(11) NOT NULL,
  `TaskName` varchar(50) NOT NULL,
  `SPName` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsSkipped` tinyint(1) DEFAULT NULL,
  `Sequence` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`TaskTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_tasktypes`
--

LOCK TABLES `tb_tasktypes` WRITE;
/*!40000 ALTER TABLE `tb_tasktypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_tasktypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_usergroups`
--

DROP TABLE IF EXISTS `tb_usergroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_usergroups` (
  `UserGroupID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` bigint(20) NOT NULL,
  `GroupID` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  PRIMARY KEY (`UserGroupID`),
  KEY `FK_tb_UserGroups_tb_Groups` (`GroupID`),
  KEY `FK_tb_UserGroups_tb_Users` (`UserID`),
  CONSTRAINT `FK_tb_UserGroups_tb_Groups` FOREIGN KEY (`GroupID`) REFERENCES `tb_groups` (`GroupID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_UserGroups_tb_Users` FOREIGN KEY (`UserID`) REFERENCES `tb_users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_usergroups`
--

LOCK TABLES `tb_usergroups` WRITE;
/*!40000 ALTER TABLE `tb_usergroups` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_usergroups` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_users`
--

LOCK TABLES `tb_users` WRITE;
/*!40000 ALTER TABLE `tb_users` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_welcomecallanswers`
--

DROP TABLE IF EXISTS `tb_welcomecallanswers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_welcomecallanswers` (
  `MerchantId` bigint(20) NOT NULL,
  `QuestionId` int(11) NOT NULL,
  `Answer` varchar(200) NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  PRIMARY KEY (`MerchantId`),
  KEY `FK_tb_WelComeCallAnswers_tb_WelComeCallQuestions` (`QuestionId`),
  CONSTRAINT `FK_Merchant_tb_welcomecallanswers_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_WelComeCallAnswers_tb_WelComeCallQuestions` FOREIGN KEY (`QuestionId`) REFERENCES `tb_welcomecallquestions` (`QuestionId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_welcomecallanswers`
--

LOCK TABLES `tb_welcomecallanswers` WRITE;
/*!40000 ALTER TABLE `tb_welcomecallanswers` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_welcomecallanswers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_welcomecallquestions`
--

DROP TABLE IF EXISTS `tb_welcomecallquestions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_welcomecallquestions` (
  `QuestionId` int(11) NOT NULL,
  `QuestionDetail` longtext NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) NOT NULL,
  `ModifyDate` datetime NOT NULL,
  PRIMARY KEY (`QuestionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_welcomecallquestions`
--

LOCK TABLES `tb_welcomecallquestions` WRITE;
/*!40000 ALTER TABLE `tb_welcomecallquestions` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_welcomecallquestions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_workflows`
--

DROP TABLE IF EXISTS `tb_workflows`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_workflows` (
  `WorkFlowId` smallint(6) NOT NULL AUTO_INCREMENT,
  `WorkFlowName` varchar(50) NOT NULL,
  `IsActive` char(10) NOT NULL,
  PRIMARY KEY (`WorkFlowId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_workflows`
--

LOCK TABLES `tb_workflows` WRITE;
/*!40000 ALTER TABLE `tb_workflows` DISABLE KEYS */;
INSERT INTO `tb_workflows` VALUES (1,'Prequal Workflow','1'),(2,'Contract Workflow','1'),(3,'Renewals Workflow','1');
/*!40000 ALTER TABLE `tb_workflows` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tmp_tb_contacts`
--

DROP TABLE IF EXISTS `tmp_tb_contacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tmp_tb_contacts` (
  `ContactId_TMP` bigint(20) NOT NULL,
  `ContactTypeId` int(11) NOT NULL,
  `JobTitle` varchar(50) DEFAULT NULL,
  `FirstName` varchar(100) NOT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) NOT NULL,
  `AddressId1` bigint(20) DEFAULT NULL,
  `DateOfbirth` date DEFAULT NULL,
  `SSN` varchar(50) DEFAULT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `IsOwner` bit(1) DEFAULT b'0',
  `IsProcessed` bit(1) DEFAULT b'0',
  PRIMARY KEY (`ContactId_TMP`),
  KEY `FK_TMP_tb_Contacts_TMP_tb_Merchants` (`MerchantId`),
  CONSTRAINT `FK_Merchant_tmp_tb_contacts_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tmp_tb_contacts`
--

LOCK TABLES `tmp_tb_contacts` WRITE;
/*!40000 ALTER TABLE `tmp_tb_contacts` DISABLE KEYS */;
/*!40000 ALTER TABLE `tmp_tb_contacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tmp_tb_merchants`
--

DROP TABLE IF EXISTS `tmp_tb_merchants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tmp_tb_merchants` (
  `MerchantId_TMP` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyId` smallint(6) DEFAULT NULL,
  `LegalName` varchar(200) NOT NULL,
  `BusinessName` varchar(200) NOT NULL,
  `SalesRepId` bigint(20) DEFAULT NULL,
  `BusinessStartDate` date DEFAULT NULL,
  `IndustryTypeId` int(11) DEFAULT NULL,
  `BusinessTypeId` int(11) DEFAULT NULL,
  `LastActivityDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `CNetProcessorNbr` varchar(20) DEFAULT NULL,
  `VNetProcessorNbr` varchar(20) DEFAULT NULL,
  `RNCNumber` varchar(11) DEFAULT NULL,
  `TelePhone1` varchar(12) DEFAULT NULL,
  `TelePhone2` varchar(12) DEFAULT NULL,
  `IsProcessed` tinyint(1) DEFAULT NULL,
  `OtherProcessorNbr` varchar(20) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `Province` varchar(50) DEFAULT NULL,
  `Country` varchar(50) DEFAULT NULL,
  `Comm_Reference` varchar(200) DEFAULT NULL,
  `Comm_Reference1` varchar(200) DEFAULT NULL,
  `Comm_Ref_Telephone` varchar(12) DEFAULT NULL,
  `Comm_Ref1_Telephone` varchar(12) DEFAULT NULL,
  PRIMARY KEY (`MerchantId_TMP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tmp_tb_merchants`
--

LOCK TABLES `tmp_tb_merchants` WRITE;
/*!40000 ALTER TABLE `tmp_tb_merchants` DISABLE KEYS */;
/*!40000 ALTER TABLE `tmp_tb_merchants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'portaldb'
--
/*!50003 DROP PROCEDURE IF EXISTS `test` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `test`()
begin
select * from `dbo_tb_tasks`;
end ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `TSK_sp_Assign_Tasks` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `TSK_sp_Assign_Tasks`()
BEGIN

	DECLARE pTaskID INT;
	DECLARE pTaskTypeID INT;
	DECLARE pWorkflowID INT;
	DECLARE pMerchantID INT;
	DECLARE pContractID INT;
	DECLARE pUserID INT;

CREATE TEMPORARY TABLE Tasks (
    TaskID bigint, 
    TaskTypeID int,
	WorkflowID int,
	MerchantID int,
	ContractID int
);
	Insert Into Tasks
	SELECT TaskID
		,TaskTypeID
		,WorkflowID
		,MerchantID
		,ContractID
	FROM dbo_tb_Tasks
	WHERE statusID = 210000 -- unassigned
	ORDER BY InsertDate;

	SET pTaskID = NULL;

	SELECT TaskID
			, TaskTypeID
			 INTO pTaskID, pTaskTypeID
		FROM Tasks limit 1;
		   
	WHILE pTaskID IS NOT NULL DO	
		SELECT UserID INTO pUserID
		FROM tb_PrequalUsers
		WHERE TaskTypeID = pTaskTypeID
		ORDER BY AssignedDate limit 1;

		UPDATE TSK_tb_Tasks
		SET AssignedUserID = pUserID
			,AssignedDate = NOW()
			,ModifyDate = NOW()
			,ModifyUserID = 2
			,StatusID = 210010 -- Assigned            
		WHERE TaskID = pTaskID;

		UPDATE tb_PrequalUsers
		SET AssignedDate = NOW()
		WHERE UserID = pUserID;

		DELETE
		FROM Tasks
		WHERE TaskID = pTaskID;

		SET pTaskID = NULL;
		SET pUserID = NULL;

		SELECT TaskID
			, TaskTypeID
			 INTO pTaskID, pTaskTypeID
		FROM Tasks limit 1;

	END while;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-09-17 10:42:58
