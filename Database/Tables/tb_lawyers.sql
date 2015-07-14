CREATE TABLE `tb_lawyers` (
  `LawyerId` bigint(20) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(200) DEFAULT NULL,
  `LastName` varchar(200) DEFAULT NULL,
  `CompanyName` varchar(200) DEFAULT NULL,
  `MerchantId` bigint(20) DEFAULT NULL,
  `ContractId` bigint(20) DEFAULT NULL,
  `DateOfAssignment` datetime DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) DEFAULT NULL,
  `DocumentIds` varchar(5000) DEFAULT NULL,
  PRIMARY KEY (`LawyerId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
