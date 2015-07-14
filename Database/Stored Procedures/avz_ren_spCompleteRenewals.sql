drop procedure if exists avz_ren_spCompleteRenewals;

delimiter $$
#call avz_ren_spCompleteRenewals (63)
create procedure avz_ren_spCompleteRenewals(iNcontractId bigint)
begin

declare iNInsertUserId bigint default 1;
declare _merchantId bigint;
SELECT merchantId
INTO _merchantId FROM
    tb_contracts
WHERE
    contractid = iNcontractId;


start transaction;
begin
		declare MaxContractId bigint;
		declare NewRenewalCntID bigint ;
		declare prefixes varchar(20);
	 

#Create New Contract
INSERT INTO `tb_contracts`
		(`ContractTypeId`,
		`ContractNumber`,
		`creationDate`,
		`MerchantId`,
		`StatusId`,
		`currencyId`,		
		#`LoanedAmount`,
		#`OwnedAmount`,
		#`PaidAmount`,
		`RenewalID`,
		`RenewalContractID`,		
		`InsertUserId`,
		`InsertDate`		
		)
		Select 13200,'',now(),MerchantId,20101,currencyId,
		#LoanedAmount,OwnedAmount,0,
		0,iNcontractId,iNInsertUserId,now()
		from tb_contracts
		where Contractid=iNcontractId;



UPDATE tb_contracts 
SET 
    ContractNumber = CONCAT('REN', LAST_INSERT_ID())
WHERE
    contractid = LAST_INSERT_ID();

Set NewRenewalCntID = LAST_INSERT_ID();


INSERT INTO `tb_renewals`(
		`RenewalId`,
		`ContractID`,
		`MerchantID`,
		`RenewedContractID`,
		`SalesRepID`,
		`InsertUserID`,
		`InsertDate`,
		`RenewalAssignedUSerID`
)
		Select RenewalID,ContractID,MerchantID,NewRenewalCntID, ifnull(AssignedUserID,0) ,iNInsertUserId,now(),ifnull(AssignedUserID,0)
		from tb_renewalslist where Contractid=iNcontractId;



#Renewal Task
call avz_tsk_spCreateTask(20,22001,1,now(),_merchantId,NewRenewalCntID,1,null,3);

#Update contracts [Applies for Renewal]
#Update tb_contracts Set statusid = 20100 where contractid=iNcontractId;	


#Associate parent contract documents to new renewal contracts 
Insert into tb_documents(DocumentTypeId,FileName,FileDetails,MerchantId,UploadUserId,UploadDate,ContractId,StatusId)
select DocumentTypeId,FileName,FileDetails,MerchantId,UploadUserId,UploadDate,NewRenewalCntID,StatusId from tb_documents
where ContractId=iNcontractId;

#Associate Bank Details with new contracts
Insert into tb_bankdetails (BankId,AccountNumber,BankCode,MerchantId,ContractId)
Select BankId,AccountNumber,BankCode,MerchantId,NewRenewalCntID from tb_bankdetails where contractId=iNcontractId;

select NewRenewalCntID ContractId;
end;

commit;
end $$ 

delimiter $$ 
