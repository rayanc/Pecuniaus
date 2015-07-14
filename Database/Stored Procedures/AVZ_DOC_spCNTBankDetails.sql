drop procedure if exists AVZ_DOC_spCNTBankDetails;

-- ================================================
-- Name: AVZ_DOC_spCNTBankDetails
-- 
-- Description : To retrieve bank details to show in Contract Workflow
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 17-09-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_DOC_spCNTBankDetails
(iNContractId bigint,
iNMerchantId bigint,
iNBankId int)
BEGIN
select 
   bd.bankDetailId, bd.accountName,bd.bankCode,bd.accountNumber,td.fileName,td.fileDetails, bd.BankId as BankID
from
    tb_bankdetails bd	
	left join tb_documents td on td.merchantId=bd.MerchantId and td.documenttypeid=8
	where bd.MerchantId=iNMerchantId and bd.ContractId= iNContractId  ;
END;
//

DELIMITER ;
