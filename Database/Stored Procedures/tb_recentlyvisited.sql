CREATE TABLE `tb_recentlyvisited` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `merchantId` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `dateVisited` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
