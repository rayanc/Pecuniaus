ALTER TABLE tb_contacts add PassportNbr varchar(100) NULL;

ALTER TABLE tb_PrequalUsers add InsertUserId bigint NULL;

ALTER TABLE tb_PrequalUsers add InsertDate DATETIME NULL;


ALTER TABLE tb_documents modify ModifyUserId bigint NULL;

ALTER TABLE tb_documents modify ModifyDate DATETIME NULL;

___________________________________________
ALTER TABLE lkp_tb_banknames add City varchar(100) NULL;
ALTER TABLE lkp_tb_banknames add Country varchar(100) NULL;



