CREATE TABLE `tb_contractsalesrep` (
  `ContractSalesRepId` bigint(20) NOT NULL AUTO_INCREMENT,
  `contractId` bigint(20) NOT NULL,
  `salesRepId` int NOT NULL,
  `IsPrimary` bit NOT NULL,
  `CreationDate` datetime NOT NULL,
  `CreatedUserId` int NOT NULL,
  PRIMARY KEY (`ContractSalesRepId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
