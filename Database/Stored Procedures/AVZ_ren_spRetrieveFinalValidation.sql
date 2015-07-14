-- ================================================
-- Name: AVZ_CNT_spFunding
-- 
-- Description : To fetch funding Details
-- 
-- Parameters : None 
--  
-- Author  : Pankaj Rana
-- 
-- Creation Date : 03-10-2014
# call AVZ_ren_spRetrieveFinalValidation(162,149)
-- ================================================
drop procedure if exists avz_ren_spRetrieveFinalValidation;
DELIMITER //
CREATE PROCEDURE avz_ren_spRetrieveFinalValidation 
(iNMerchantId bigint,
iNContractId bigint)
BEGIN
#declare BLADocumentId int;
#declare BLAFile varchar(200);
#declare BLADocumentTypeId int;

#select distinct DocumentId,documenttypeid,FileName into BLADocumentId, BLADocumentTypeId,BLAFile from tb_documents 
#where contractId=57;

Select ltb.bankName, bd.accountNumber, bd.accountName, ct.LoanedAmount as mcaAmount,
lte.expenseAmount, -- administrative expenses,
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
	Left join tb_documents doc on ct.MerchantId = doc.MERCHANTID and doc.ContractId=ct.ContractId and doc.DocumentTypeId = 14
	Left Join (select distinct DocumentId,documenttypeid,FileName,contractId from tb_documents 
	where contractId=iNContractId and documenttypeid = 20 )as bla on ct.ContractId=bla.contractId
where    
/*ct.MerchantId=iNMerchantId and*/
 ct.ContractId=iNContractId and ow.IsAuthorized=1;

END;
//
DELIMITER ;
