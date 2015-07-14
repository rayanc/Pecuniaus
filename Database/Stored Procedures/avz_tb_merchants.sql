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
  `accountId` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`MerchantId`),
  KEY `FK_tb_Merchants_Gen_tb_Addresses` (`AddressId`),
  KEY `FK_tb_Merchants_Gen_tb_Statuses` (`StatusId`),
  KEY `FK_tb_Merchants_tb_SalesRep` (`SalesRepId`),
  KEY `FK_BusinessTypeID_lkp_tb_EntityTypes_idx` (`BusinessTypeId`),
  CONSTRAINT `FK_BusinessTypeID_lkp_tb_EntityTypes` FOREIGN KEY (`BusinessTypeId`) REFERENCES `lkp_tb_entitytypes` (`EntitytypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tb_Merchants_Gen_tb_Statuses` FOREIGN KEY (`StatusId`) REFERENCES `lkp_tb_statuses` (`StatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


-- Dump completed on 2014-10-01 16:37:57
