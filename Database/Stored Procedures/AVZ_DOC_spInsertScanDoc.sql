-- ================================================
-- Name: AVZ_DOC_spInsertScanDoc
-- 
-- Description : To Insert Application form of Merchant
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 10-09-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_DOC_spInsertScanDoc 
(
 iNMerchantID bigint,
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
`UploadDate`)
VALUES
(1,iNFileName,iNFileDetails,iNMerchantID,iNUploadUserId,NOW());
	 
END;
//
DELIMITER ;
