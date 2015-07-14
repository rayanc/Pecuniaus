drop procedure if exists avz_cnt_spInsertVerificationScript;
delimiter $$

Create procedure avz_cnt_spInsertVerificationScript(
iNmerchantId bigint,
iNcontractId bigint,
iNscriptFile bigint,
iNentityFor bigint
)

begin

if ((select count(1) from tb_verification_Script where MerchantId=iNmerchantId and ContractId=iNcontractId and EntityFor=iNentityFor)=0) then
insert into tb_verification_Script(MerchantId,ContractId,ScriptFile,EntityFor)
values(iNmerchantId,iNcontractId,iNscriptFile,iNentityFor);
else
update tb_verification_Script set ScriptFile= iNscriptFile where MerchantId=iNmerchantId and ContractId=iNcontractId and EntityFor=iNentityFor;

end if;

end;

