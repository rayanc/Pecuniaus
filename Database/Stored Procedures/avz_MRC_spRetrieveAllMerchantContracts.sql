drop procedure if exists avz_MRC_spRetrieveAllMerchantContracts;

delimiter $$

create procedure avz_MRC_spRetrieveAllMerchantContracts
(iNmerchantId bigint)
begin 
SELECT contractId as keyId,ContractNumber as description FROM tb_contracts where merchantId=iNmerchantId;

end $$
delimiter $$
