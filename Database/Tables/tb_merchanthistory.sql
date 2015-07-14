CREATE TABLE `tb_merchanthistory` (
  `HistoryId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) NOT NULL,
  `HistoryDate` datetime NOT NULL,
  `Field` varchar(200) NOT NULL,
  `OldValue` varchar(1000) NOT NULL,
  `NewValue` varchar(1000) NOT NULL,
  PRIMARY KEY (`HistoryId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
