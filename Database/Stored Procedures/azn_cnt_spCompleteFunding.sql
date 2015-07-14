drop procedure if exists azn_cnt_spCompleteFunding;
delimiter $$

CREATE PROCEDURE azn_cnt_spCompleteFunding
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
	iNAction varchar (50),
    iNUserId int
)

BEGIN

	/*
	Update tb_contracts Set
	StatusId = case when  iNContractReviewed = 1 and iNContractFunded = 1 then 2007 else StatusId end
	where contractid=iNContractId; 
	*/  

	declare _merchantId bigint;
	declare _contractStatus int default 20001;
	declare _receiveBy bigint;
	declare _completeBy bigint;
	declare _oldStatusId int;

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

	SELECT StatusId, CNTReceivedBy, FundingCompleteBy INTO _oldStatusId, _receiveBy, _completeBy FROM tb_contracts WHERE ContractId=iNContractId;
    
	if (iNContractReviewed=1)then
		Set _contractStatus = 20004 ;
        if (_oldStatusId != _contractStatus) then
			set _receiveBy = iNUserId;
        end if;
	end if;
	if (iNContractFunded =1) then
		Set _contractStatus = 20007;
		 if (_oldStatusId != _contractStatus) then
			set _completeBy = iNUserId;
        end if;
        
	end if;

	#Update Contract 
	Update tb_contracts set `LoanedAmount` = iNMcaAmount , `OwnedAmount` = iNTotalOwnedAmount, 
		AdministrativeExpenses=iNExpenseAmount,
		StatusId = case  when _contractStatus = 0 then StatusId else _contractStatus end,
		CNTReceivedBy = _receiveBy,
		FundingCompleteBy = _completeBy        
	where ContractId=iNContractId;

	if (_contractStatus AND iNAction='complete') then
		Update tb_contracts set `LoanedAmount` = iNMcaAmount , `OwnedAmount` = iNTotalOwnedAmount, 
        fundingDate= date_format(now(),'%Y-%m-%d')
        where ContractId=iNContractId;

		#Will Colose previous contract

		#Complete task - 17: CW - Funding
		call avz_tsk_spCompleteTask(_merchantId,17,2,iNContractId);

		#Create Task 18- CW - Final Validation
		call avz_tsk_spCreateTask(18,22002,1,now(),_merchantId,iNContractId,1,null,2);

	end  if;
end 
$$
DELIMITER ;
