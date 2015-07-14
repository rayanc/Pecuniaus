-- ================================================
-- call avz_ren_spFunding(200,102)
-- ================================================
drop procedure if exists avz_ren_spFunding;
DELIMITER //
CREATE PROCEDURE avz_ren_spFunding  
(iNMerchantId bigint,
iNContractId bigint)
BEGIN
declare balance double;
select ifnull(ownedAmount,0)-ifnull(paidAmount,0) into balance from tb_contracts where merchantId=iNMerchantId and
StatusId=20007 order by contractId desc limit 1;

Select ltb.bankName, bd.accountNumber , bd.accountName, ct.OwnedAmount as mcaAmount,
ifnull(lte.expenseAmount,0) expenseAmount, -- administrative expenses,
(ct.OwnedAmount -balance -ifnull(lte.expenseAmount,0)) as totalFundingAmount, -- total funding amount
GROUP_CONCAT(CONCAT_WS(' ', co.FirstName, co.MiddleName, co.LastName) SEPARATOR ', ') AS ownerName
,ct.contractId,ltb.bankId,ow.ownerId,doc.documentId,doc.fileName,doc.documentTypeId,doc.FileDetails,ct.merchantId,ct.StatusId as contractStatusId

from  tb_contracts ct		
	left join tb_bankdetails bd on ct.ContractId = bd.ContractId
	left join lkp_tb_banknames ltb on bd.BankId = ltb.BankId
	left join (select * from lkp_tb_expenses )lte on  lte.ContractTypeId = ct.ContractTypeId  and MinimumFundingAmount<= ifnull(ct.LoanedAmount,-1) and MaximumFundingAmount >= ifnull(ct.LoanedAmount,-1)
				
	left join tb_owners ow on ct.MerchantId=ow.MerchantId
	left join tb_contacts co on ow.ContactId = co.ContactId
	Left join tb_documents doc on ct.MerchantId = doc.MERCHANTID and doc.ContractId=ct.ContractId and doc.DocumentTypeId = 16
where    
 ct.ContractId=iNContractId and ow.IsAuthorized=1 ;

END;
//
DELIMITER ;
