-- ================================================
-- Name: AVZ_COL_spRetrieveLawyersWithDocs
-- 
-- Description : To Retrieve lawyers list to assign them for collections
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spRetrieveLawyersWithDocs;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spRetrieveLawyersWithDocs
(iNMerchantId bigint,
iNContractId bigint
)
BEGIN

select lw.lawyerId, lw.firstName, lw.lastName, lw.companyName
from tb_lawyers lw
-- left join lkp_tb_lawyerscompany tlc on lw.CompanyId = tlc.CompanyId
order by lw.companyName;

select td.documentId, dtt.Description as documentType
from tb_documents td
left join lkp_tb_documenttypes dtt on td.DocumentTypeId = dtt.DocumentTypeId
where td.MerchantId = iNMerchantId and td.ContractId=iNContractId;

END$$
DELIMITER ;