
drop procedure if exists avz_MRC_spretrieveMerchantDocumentsInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantDocumentsInformation(iNmerchantId bigint,iNdocumentTypeId int)
begin

Select 
DocumentId,FileName as DocumentName,TYP.Description as DocumentType,UploadDate,UserName as UploadedBy
from tb_documents DC
Inner Join lkp_tb_documenttypes TYP on TYP.DocumentTypeId=DC.DocumentTypeId
Inner Join tb_users USR on USR.ID=DC.UploadUserId
where DC.FileName <> '' and DC.MerchantId=iNmerchantId and TYP.DocumentTypeId=IF(iNdocumentTypeId=0,TYP.DocumentTypeId,iNdocumentTypeId);

end;