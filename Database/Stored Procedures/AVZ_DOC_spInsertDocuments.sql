-- ================================================
-- Name: AVZ_DOC_spInsertDocuments
-- 
-- Description : To Insert documents for a Merchant
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 10-09-2014
-- ================================================
drop procedure if exists AVZ_DOC_spInsertDocuments;

DELIMITER //
CREATE PROCEDURE AVZ_DOC_spInsertDocuments 
(
 iNDocumentTypeID int,
 iNMerchantID bigint,
 iNContractID bigint,
 iNFileName varchar(1000),
 iNFileDetails varchar(5000),
 iNUploadUserId bigint
)
BEGIN  	 
INSERT INTO `tb_documents`
(`DocumentTypeId`,
`FileName`,
`FileDetails`,
`MERCHANTID`,
`UploaduserId`,
`UploadDate`,
`ContractID`,
`StatusId`)
VALUES
(iNDocumentTypeID,iNFileName,iNFileDetails,iNMerchantID,iNUploadUserId,now(),iNContractID,110002);
	 
END;
//
DELIMITER ;
