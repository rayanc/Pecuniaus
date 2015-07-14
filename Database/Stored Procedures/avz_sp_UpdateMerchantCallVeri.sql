drop procedure if exists avz_sp_UpdateMerchantCallVeri;
delimiter $$
create procedure avz_sp_UpdateMerchantCallVeri(
iNmerchantId bigint,
iNContractId bigint,
iNOwnerFirstName varchar(200), 
iNOwnerLastName varchar(200),
iNOwnerPassport varchar(20), 
iNLegalName varchar(200),
iNBusinessName varchar(200), 
-- iNIsAuthorised bit,
iNBusinessStartDate datetime, 
iNGrossAnnualSales numeric, 
iNTypeOfAdvanceId bigint, 
iNLoanAmount numeric, 
iNOwnedAmount numeric, 
iNRetensionPct numeric, 
iNAdminExp numeric,
iNScriptFile varchar(200), 
iNAnswersXml text,
iNisCompleted smallint
)
begin
	declare pOwnerContactId bigint;
	declare pContractId bigint;

	select ContactId into pOwnerContactId from tb_owners where MerchantId = iNmerchantId and IsAuthorized=1;
	select ContractId into pContractId from tb_verificationScript where ContractId = iNContractId and EntityFor='Contract';

	update tb_merchants set LegalName=iNLegalName,BusinessName=iNBusinessName,businessStartDate=iNBusinessStartDate,GrossAnnualSales=iNGrossAnnualSales where MerchantId = iNmerchantId;	
	update tb_contacts set FirstName=iNOwnerFirstName, LastName = iNOwnerLastName,PassportNbr=iNOwnerPassport where ContactId = pOwnerContactId;
	update tb_contracts set TypeOfAdvances=iNTypeOfAdvanceId,LoanedAmount=iNLoanAmount,OwnedAmount=iNOwnedAmount,AdministrativeExpenses=iNAdminExp where MerchantId = iNmerchantId and ContractId=iNContractId;
	
	update tb_offers set Retention= iNRetensionPct where ContractId=iNContractId and status=190001;
	if pContractId > 0 then
	update tb_verificationScript set ScriptFile=iNScriptFile where ContractId = iNContractId and EntityFor='Contract';
else
	insert into tb_verificationScript (ContractId, ScriptFile, EntityFor) values (iNContractId,iNScriptFile,'Contract');
end if;

	call avz_sp_UpdateCallVeriAns (iNmerchantId, iNContractId, iNAnswersXml, 'Contract');

	 if iNisCompleted = 1 then	
		call avz_tsk_spCompleteTask(iNMerchantId,9,2,iNContractId);
     end if;
end;
$$
