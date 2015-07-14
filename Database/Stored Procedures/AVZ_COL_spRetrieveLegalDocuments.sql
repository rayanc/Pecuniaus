-- ================================================
-- Name: AVZ_COL_spRetrieveLegalDocuments
-- 
-- Description : To Retrieve legal documents to assign to lawyers
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spRetrieveLegalDocuments;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spRetrieveLegalDocuments
(iNMerchantId bigint,
iNContractId bigint
)
BEGIN

select td.documentId, dtt.Description as documentType
from tb_documents td
left join lkp_tb_documenttypes dtt on td.DocumentTypeId = dtt.DocumentTypeId
where td.MerchantId = iNMerchantId and td.ContractId=iNContractId;

END$$
DELIMITER ;