drop procedure if exists avz_sp_UpdateLandLordlVeri;
delimiter $$
create procedure avz_sp_UpdateLandLordlVeri(
iNMerchantId bigint,
iNContractId bigint,
iNOwnerFirstName varchar(200), 
iNOwnerLastName varchar(200), 
iNMerchantAddress varchar(200), 
iNLandLordFirstName varchar(200), 
iNLandLordLastName varchar(200), 
iNLegalName varchar(200),
iNRentedAmount double,
iNContractStartDate datetime,
iNContractExpireDate datetime, 
iNAnswersXml text, 
iNScriptFile varchar(200),
iNisCompleted smallint
)
begin
	
declare pOwnerContactId bigint;
declare pLandLordContactId bigint;	
declare pAddressId bigint;
declare pContractId bigint;

set pContractId = 0;

select ContactId into pOwnerContactId from tb_owners where MerchantId = iNmerchantId and IsAuthorized=1;
select ContactId into pLandLordContactId from tb_merchantlandlords where MerchantId = iNmerchantId;
select AddressId into pAddressId from tb_merchants where MerchantId = iNmerchantId;
select ContractId into pContractId from tb_verificationScript where ContractId = iNContractId and EntityFor='LandLord';

update tb_merchantlandlords set BuildingName=iNLegalName where merchantId = iNmerchantId;
update tb_merchants set RentedAmount=iNRentedAmount where MerchantId = iNmerchantId;
update tb_contacts set FirstName=iNOwnerFirstName, LastName = iNOwnerLastName where ContactId = pOwnerContactId;
update tb_contacts set FirstName=iNLandLordFirstName, LastName = iNLandLordLastName where ContactId = pLandLordContactId;
update tb_addresses set Address1 = iNMerchantAddress where AddressId=pAddressId;
update tb_contracts set fundingDate = iNContractStartDate, WrittenOffDate=iNContractExpireDate where MerchantId = iNmerchantId and contractId=iNContractId;


if pContractId > 0 then
	update tb_verificationScript set ScriptFile=iNScriptFile where ContractId = iNContractId and EntityFor='LandLord';
else
	insert into tb_verificationScript (ContractId, ScriptFile, EntityFor) values (iNContractId,iNScriptFile,'LandLord');
end if;

call avz_sp_UpdateCallVeriAns (iNmerchantId, iNContractId, iNAnswersXml, 'LandLord');

	 if iNisCompleted = 1 then	
		call avz_tsk_spCompleteTask(iNMerchantId,14,2,iNContractId);
     end if;
end;
$$
