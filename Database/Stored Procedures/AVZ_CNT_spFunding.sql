-- ================================================
-- call avz_ren_spFunding(200,102)
-- ================================================
drop procedure if exists AVZ_CNT_spFunding;
DELIMITER //
CREATE  PROCEDURE `AVZ_CNT_spFunding`(
	iNMerchantId bigint,
	iNContractId bigint
)
BEGIN

Select ltb.bankName, 
	bd.accountNumber, 
	bd.accountName, 
	ct.OwnedAmount as mcaAmount,
	IFNULL(ct.AdministrativeExpenses,	(select  expenseAmount from lkp_tb_expenses where  lkp_tb_expenses.ContractTypeID= ct.ContractTypeId limit 1)) as expenseAmount, -- administrative expenses,
	ct.OwnedAmount - ct.AdministrativeExpenses as totalFundingAmount, -- total funding amount
	GROUP_CONCAT(CONCAT_WS(' ', co.FirstName, co.MiddleName, co.LastName) SEPARATOR ', ') AS ownerName,
	ct.contractId,
	ltb.bankId,
	ow.ownerId,
	doc.documentId,
	doc.fileName,
	doc.documentTypeId,
	doc.FileDetails,
	ct.merchantId,
	ct.StatusId as contractStatusId,
    u.UserName as ReviewedBy,
    uf.UserName as CompletedBy

from
    tb_contracts ct		
	left join tb_bankdetails bd on ct.MerchantId = bd.MerchantId and bd.ContractId = iNContractId
	left join lkp_tb_banknames ltb on bd.BankId = ltb.BankId
	/*left join lkp_tb_expenses lte on ct.ContractTypeId=lte.ContractTypeID*/
	left join tb_owners ow on ct.MerchantId=ow.MerchantId
	left join tb_contacts co on ow.ContactId = co.ContactId
	Left join tb_documents doc on ct.MerchantId = doc.MERCHANTID and doc.ContractId=ct.ContractId and doc.DocumentTypeId = 17
	left join tb_users u on ct.CNTReceivedBy = u.ID
	left join tb_users uf on ct.FundingCompleteBy= uf.ID
    
where    
/*ct.MerchantId=iNMerchantId and*/
 ct.ContractId=iNContractId and ow.IsAuthorized=1;

END
