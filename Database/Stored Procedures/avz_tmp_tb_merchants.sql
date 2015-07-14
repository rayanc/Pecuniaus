--
-- Table structure for table `tmp_tb_merchants`
--

DROP TABLE IF EXISTS `tmp_tb_merchants`;

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
  `TelePhone1` varchar(20) DEFAULT NULL,
  `TelePhone2` varchar(20) DEFAULT NULL,
  `IsProcessed` tinyint(1) DEFAULT NULL,
  `OtherProcessorNbr` varchar(20) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `Province` varchar(50) DEFAULT NULL,
  `Country` varchar(50) DEFAULT NULL,
  `Comm_Reference` varchar(200) DEFAULT NULL,
  `Comm_Reference1` varchar(200) DEFAULT NULL,
  `Comm_Ref_Telephone` varchar(12) DEFAULT NULL,
  `Comm_Ref1_Telephone` varchar(12) DEFAULT NULL,
  `businessType` varchar(200) DEFAULT NULL,
  `industrytype` varchar(200) DEFAULT NULL,
  `accountId` varchar(200) DEFAULT NULL,
  `addressline1` varchar(200) DEFAULT NULL,
  `addressline2` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`MerchantId_TMP`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;


-- Dump completed on 2014-10-01 16:37:57
