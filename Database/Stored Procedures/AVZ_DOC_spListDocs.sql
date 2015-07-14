drop procedure if exists AVZ_DOC_spListDocs;
-- ================================================
-- Name: AVZ_DOC_spListDocs
-- 
-- Description : List all the dcouments for particular Merchant Id. 
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-09-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_DOC_spListDocs
(iNMerchantID bigint,
 iNContractId bigint,
 iNDocumentTypeId int)
BEGIN
select 
    fileName, fileDetails, documentId, StatusId
from
    tb_documents         
where
    MerchantId = iNMerchantId
	and (ContractId=0 or ContractId = iNContractId)
    and DocumentTypeId = iNdocumentTypeId;
END;
//

DELIMITER ;
