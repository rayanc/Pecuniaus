CREATE TABLE `tb_paymentagreement` (
  `PaymentId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) DEFAULT NULL,
  `ContractId` varchar(45) DEFAULT NULL,
  `DateOfAgreement` datetime DEFAULT NULL,
  `StartDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `DaysOfInterval` int(11) DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`PaymentId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
