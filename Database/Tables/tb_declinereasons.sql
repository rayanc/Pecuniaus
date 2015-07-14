CREATE TABLE  `tb_declinereasons` (
  `id` BIGINT NOT NULL AUTO_INCREMENT,
  `contractId` BIGINT NULL,
  `workflowid` BIGINT NULL,
  `declineNotes` VARCHAR(200) NULL,
  `declinereasonid` BIGINT NULL,
  PRIMARY KEY (`id`));