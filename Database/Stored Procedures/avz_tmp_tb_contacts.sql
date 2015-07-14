
DROP TABLE IF EXISTS `tmp_tb_contacts`;

CREATE TABLE `tmp_tb_contacts` (
  `ContactId_TMP` bigint(20) NOT NULL AUTO_INCREMENT,
  `ContactTypeId` int(11) DEFAULT NULL,
  `JobTitle` varchar(50) DEFAULT NULL,
  `FirstName` varchar(100) NOT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) NOT NULL,
  `AddressId1` bigint(20) DEFAULT NULL,
  `DateOfbirth` date DEFAULT NULL,
  `SSN` varchar(50) DEFAULT NULL,
  `MerchantId` bigint(20) DEFAULT NULL,
  `IsOwner` bit(1) DEFAULT b'0',
  `IsProcessed` bit(1) DEFAULT b'0',
  PRIMARY KEY (`ContactId_TMP`),
  KEY `FK_TMP_tb_Contacts_TMP_tb_Merchants` (`MerchantId`)
) 
