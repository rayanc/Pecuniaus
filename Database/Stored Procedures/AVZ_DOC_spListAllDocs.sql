-- ================================================
-- Name: AVZ_DOC_spListAllDocs
-- 
-- Description : List all the dcouments for particular Merchant Id. 
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-09-2014
-- Updated by Amit (sachin verma) added more columns like uploaddate, uploadedby
-- and corrected the join as it has taken column which is not exist
-- Updated date : 28 oct 2014
-- ================================================
DROP procedure IF EXISTS AVZ_DOC_spListAllDocs;

DELIMITER $$

CREATE PROCEDURE AVZ_DOC_spListAllDocs
(iNMerchantID bigint,
 iNContractId bigint
 )
 
BEGIN
select 
    d.fileName, d.fileDetails, d.documentId, dtt.description as documentName, dtt.documentTypeId
,d.UploadDate as uploadedDate,users.UserName as uploadedBy, StatusId
from
    Pecuniaus.tb_documents d
	join Pecuniaus.lkp_tb_documenttypes dtt on d.DocumentTypeId= dtt.DocumentTypeId
   left join Pecuniaus.tb_users users on d.UploaduserId=users.ID  
   where d.merchantId=iNMerchantID and d.contractid=iNContractId;
END;

