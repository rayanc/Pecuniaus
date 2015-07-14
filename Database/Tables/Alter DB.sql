CREATE TABLE tb_PrequalUsers (
	UserID bigint NULL
	,TaskTypeID int NULL
	,AssignedDate datetime NULL
	,WorkFlowID int NULL
	,InsertUserId bigint NULL
	); 

CREATE TABLE `tb_verificationquestions` (
  `QuestionId` bigint(20) NOT NULL AUTO_INCREMENT,
  `Entity` varchar(100) DEFAULT NULL,
  `Description` varchar(5000) DEFAULT NULL,
  `QuestionType` varchar(100) DEFAULT NULL,
  `isActive` bit DEFAULT NULL,
  `InsertDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`QuestionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_verificationanswers` (
  `AnswerId` bigint(20) NOT NULL AUTO_INCREMENT,
  `QuestionId` bigint(20) DEFAULT NULL,
  `AnswerText` varchar(5000) DEFAULT NULL,
  `MerchantId` bigint DEFAULT NULL,
  `ContractId` bigint DEFAULT NULL,
  `InsertDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`AnswerId`),
  KEY `QuestionID_idx` (`QuestionId`),
  CONSTRAINT `tb_verificationAnswers_FK_tb_verificationquestions_QuestionId` FOREIGN KEY (`QuestionId`) REFERENCES `tb_verificationquestions` (`QuestionId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  KEY `ContractId_idx` (`ContractId`),
  CONSTRAINT `tb_verificationAnswers_FK_tb_contracts_ContractId` FOREIGN KEY (`ContractId`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_bankdetails` (
  `BankDetailId` int(11) NOT NULL AUTO_INCREMENT,
  `BankId` int(11) DEFAULT NULL,
  `AccountNumber` varchar(100) DEFAULT NULL,
  `AccountName` varchar(200) DEFAULT NULL,
  `MerchantId` bigint(20) DEFAULT NULL,
  `ContractId` bigint(20) DEFAULT NULL,
  `InsertDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`BankDetailId`),
  KEY `tb_BankDetails_tb_Merchants_MerchantId_idx` (`MerchantId`),
  CONSTRAINT `tb_BankDetails_tb_Merchants_MerchantId` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE lkp_tb_BankNames (
	`BankId` int(11) NOT NULL AUTO_INCREMENT
	,`BankName` varchar(500) DEFAULT NULL
	,`InsertDate` datetime DEFAULT CURRENT_TIMESTAMP
	,PRIMARY KEY (`BankId`)	
	); 


ALTER TABLE tb_Tasks modify AssignedUserId BIGINT NULL;

ALTER TABLE tb_Tasks modify AssignedDate DATETIME NULL;

ALTER TABLE lkp_tb_documenttypes add isRequired bit NULL;

ALTER TABLE tb_documents modify FileName varchar(1000) NULL;

ALTER TABLE tb_documents modify FileDetails varchar(5000) NULL;

ALTER TABLE tb_documents add ContractId bigint NULL;




