-- ================================================
-- Name: AVZ_FIN_spGetFundedContracts
-- 
-- Description : To get list of funded contracts of a Merchant
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 29-04-2015
-- ================================================
Drop procedure if exists AVZ_FIN_spGetFundedContracts;

DELIMITER $$
CREATE PROCEDURE AVZ_FIN_spGetFundedContracts
(iNMerchantId bigint
)
BEGIN
Declare pContractId bigint;
select avz_gen_fnRetrieveActiveContract(iNMerchantId) into pContractId;
if (pContractId > 0) then
	select pContractId as contractId,
    ifnull(ContractTypeID,0) contractTypeId,ifnull(PaidAmount,0) as creditTransferAmount,
    ifnull((contr.ownedAmount - IFNULL(contr.Paidamount, 0)),0) as pendingAmount,
	pro.ProcessorTypeId as processorId
	from tb_contracts contr
	inner join tb_processor pro on pro.MerchantId=iNMerchantId
	where ContractId=pContractId limit 1;
else
	select 0 as contractId;


end if;
END$$
DELIMITER ;