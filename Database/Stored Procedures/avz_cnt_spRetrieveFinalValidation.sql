-- ================================================
# call avz_cnt_spRetrieveFinalValidation(34,73)
-- ================================================
drop procedure if exists avz_cnt_spRetrieveFinalValidation;
DELIMITER //
CREATE PROCEDURE avz_cnt_spRetrieveFinalValidation 
(iNMerchantId bigint,
iNContractId bigint)
BEGIN


Select ltb.bankName, bd.accountNumber, bd.accountName, ct.LoanedAmount as mcaAmount,
ct.AdministrativeExpenses as expenseAmount, -- administrative expenses,
ct.OwnedAmount as totalFundingAmount, -- total funding amount
GROUP_CONCAT(CONCAT_WS(' ', co.FirstName, co.MiddleName, co.LastName) SEPARATOR ', ') AS ownerName
,ct.contractId,ltb.bankId,ow.ownerId,doc.fileName as legalFile,doc.documentTypeId,doc.documentId
,bla.DocumentId as BLADocumentId,bla.FileName as BLAFile,bla.documenttypeid as BLADocumentTypeId

from
    tb_contracts ct		
	left join tb_bankdetails bd on ct.ContractId = bd.ContractId
	left join lkp_tb_banknames ltb on bd.BankId = ltb.BankId
	left join lkp_tb_expenses lte on ct.ContractTypeId=lte.ContractTypeID
	left join tb_owners ow on ct.MerchantId=ow.MerchantId
	left join tb_contacts co on ow.ContactId = co.ContactId
	Left join (select distinct DocumentId,documenttypeid,FileName,contractId from tb_documents 
	where contractId=iNContractId and documenttypeid = 19)as doc on ct.ContractId=doc.contractId
	Left Join (select distinct DocumentId,documenttypeid,FileName,contractId from tb_documents 
	where contractId=iNContractId and documenttypeid = 18)as bla on ct.ContractId=bla.contractId
where    
/*ct.MerchantId=iNMerchantId and*/
 ct.ContractId=iNContractId and ow.IsAuthorized=1;



END;
//
DELIMITER ;
