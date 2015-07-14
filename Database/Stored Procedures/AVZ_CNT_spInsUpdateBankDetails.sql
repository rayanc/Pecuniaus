drop procedure if exists AVZ_CNT_spInsUpdateBankDetails;
-- ================================================
-- Name: AVZ_CNT_spInsUpdateBankDetails
-- 
-- Description : To Insert Update bank details for a particular contract
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 10-09-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_CNT_spInsUpdateBankDetails 
	(iNBankDetailId int, 
    iNBankId int,
	iNAccountName varchar(500),
	iNBankCode varchar(100),
	iNAccountNumber varchar(500),	
	iNMerchantId bigint(20),
    iNContractId bigint(20),
	iNisCompleted smallint
	)
BEGIN

if(iNBankDetailId=0) then
     insert into  tb_bankdetails(BankDetailId,BankId, AccountName, BankCode, AccountNumber, MerchantId, ContractId,inserdate)
	 values (iNBankDetailId,iNBankId,iNAccountName,iNBankCode,iNAccountNumber,iNMerchantId,iNContractId, date_format(now(),'%y-%m-%d'));
	 /*on duplicate key update BankDetailId = values(BankDetailId), BankId = values(BankId), AccountName = values(AccountName), BankCode = values(BankCode),  
	 AccountNumber = values(AccountNumber), MerchantId = values(MerchantId),  ContractId = values(ContractId); */
else

	update tb_bankdetails set BankId=iNBankId,AccountName=iNAccountName,BankCode=iNBankCode,AccountNumber=iNAccountNumber,Modifydate=date_format(now(),'%y-%m-%d') where MerchantId=iNMerchantId;

end if;	 
	 if iNisCompleted = 1 then	
		call avz_tsk_spCompleteTask(iNMerchantId,11,2,iNContractId);
     end if;
END;
//
DELIMITER ;
