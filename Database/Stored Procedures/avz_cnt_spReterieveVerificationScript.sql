drop procedure if exists avz_cnt_spReterieveVerificationScript;
delimiter $$

Create procedure avz_cnt_spReterieveVerificationScript(
iNmerchantId bigint,
iNcontractId bigint,
iNentityFor bigint
)

begin

select  ScriptFile from tb_verification_Script where MerchantId=iNmerchantId and ContractId=iNcontractId and EntityFor=iNentityFor;

end;