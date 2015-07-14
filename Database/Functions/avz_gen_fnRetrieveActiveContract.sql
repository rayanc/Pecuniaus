drop function if exists avz_gen_fnRetrieveActiveContract;

DELIMITER $$

CREATE FUNCTION avz_gen_fnRetrieveActiveContract (iNmerchantId bigint)
RETURNS BIGINT 
BEGIN
Declare SID,CID int;
select StatusId into SID from lkp_tb_statuses where StatusTypeId='CNT' and StatusName='Funded';
if exists(select ContractId from tb_contracts where MerchantId=iNmerchantId and StatusId=SID) then
begin
select max(ContractId) into CID from tb_contracts where MerchantId=iNmerchantId and StatusId=SID;
end;
else
begin
select max(ContractId) into CID from tb_contracts where MerchantId=iNmerchantId;
end; 
end if;
return CID;

END