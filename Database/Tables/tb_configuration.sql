CREATE TABLE `tb_configuration` (
  `ConfigId` bigint(20) NOT NULL AUTO_INCREMENT,
  `Config` varchar(100) DEFAULT NULL,
  `ConfigSystem` varchar(45) DEFAULT NULL,
  `Value` varchar(300) DEFAULT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`ConfigId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
