-- ================================================
-- Name: AVZ_COL_spRetrieveAssignedLawyers
-- 
-- Description : To Retrieve lawyers list to assign them for collections
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spRetrieveAssignedLawyers;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spRetrieveAssignedLawyers
(iNMerchantId bigint,
iNContractId bigint
)
BEGIN

select lw.lawyerId, lw.firstName, lw.lastName, lw.companyName, lw.dateOfAssignment, lw.DocumentIds as documentType
from tb_lawyers lw
where lw.MerchantId=iNMerchantId and lw.ContractId=iNContractId;

END$$
DELIMITER ;