-- ================================================
-- Name: AVZ_COL_spAssignLawyers
-- 
-- Description : To update lawyers list to assign them for collections
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spAssignLawyers;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spAssignLawyers
(iNMerchantId bigint,
iNContractId bigint,
iNLawyerId bigint,
iNDocumentIds varchar(5000),
iNInsertUserId bigint
)
BEGIN

update tb_lawyers 
set 
    MerchantId = iNMerchantId,
    ContractId = iNContractId,
    DateOfAssignment = now(),
	DocumentIds = iNDocumentIds,
	insertUserId = iNInsertUserId
where
    LawyerId = iNLawyerId; 

END$$
DELIMITER ;