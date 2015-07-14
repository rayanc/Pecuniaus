drop procedure if exists azn_ren_spCompleteFunding;
delimiter $$
#call azn_ren_spCompleteFunding(32,42,1,'1111','Account Name',10000,2000,2002,1,1,0)
CREATE PROCEDURE azn_ren_spCompleteFunding
(
iNMerchantId int,
iNContractId int,
iNBankId int,
iNAccountNumber varchar(100),
iNAccountName varchar(500),
iNMcaAmount double,
iNExpenseAmount double,
iNTotalOwnedAmount double,
iNOwnerId int,
iNContractReviewed bit,
iNContractFunded bit,
iNAction varchar (50)
)

BEGIN

/*
Update tb_contracts Set
StatusId = case when  iNContractReviewed = 1 and iNContractFunded = 1 then 2007 else StatusId end
where contractid=iNContractId; 
*/  

declare _merchantId bigint;
declare _contractStatus int;

SELECT merchantId
INTO _merchantId FROM   tb_contracts  WHERE     contractid = iNContractId;

#insert bank details
If exists (select 1 from tb_bankdetails where ContractId= iNContractId )=0 then
	Insert into tb_bankdetails ( `BankId`,`AccountNumber`,`AccountName`,`MerchantId`,`ContractId`,`InsertDate`)
	Values (iNBankId,iNAccountNumber,iNAccountName,iNMerchantId,iNContractId,now());
else
	update tb_bankdetails set `BankId`= iNBankId,`AccountNumber`=iNAccountNumber ,`AccountName`=iNAccountName ,`ModifyDate`=now()
	where ContractId=iNContractId;
end if ;

if (iNContractReviewed=1)then
	Set _contractStatus = 20004 ;
end if;
if (iNContractFunded =1) then
	Set _contractStatus = 20007;
end if;

#Update Contract 
Update tb_contracts set `LoanedAmount` = iNMcaAmount , `OwnedAmount` = iNTotalOwnedAmount, AdministrativeExpenses=iNExpenseAmount,
StatusId = case  when _contractStatus = 0 then StatusId else _contractStatus end 
where ContractId=iNContractId;


#if (_contractStatus = 20007  AND iNAction='complete') then


#Will Colose previous contract

#Complete task - 25: RW - Funding
#call avz_tsk_spCompleteTask(_merchantId,25,3,iNContractId);

#Create Task 26- RW - Final Validation -
#call avz_tsk_spCreateTask(26,22002,1,now(),_merchantId,iNContractId,1,null,3);

#end  if;
end 
$$
DELIMITER ;
