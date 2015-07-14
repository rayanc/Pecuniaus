DROP procedure IF EXISTS `AVZ_DOC_spUpdateDocs`;

DELIMITER $$

CREATE PROCEDURE AVZ_DOC_spUpdateDocs
(
iNDocumentTypeID bigint,
iNMerchantID bigint,
 iNFileName varchar(1000),
 iNFileDetails varchar(5000),
 iNUploadUserId bigint,
 iNDocumentId bigint,
 iNContractId bigint,
 iNStatusId bigint
 )

if iNDocumentId=0 then
begin
insert into tb_documents(
FileName,
FileDetails,
ModifyUserId,
Modifydate,
MerchantID,
StatusId,
DocumentTypeId,
contractId,
UploadUserId,
UploadDate
) 
values(
iNFileName,
iNFileDetails,
iNUploadUserId,
now(),
iNMerchantID,
iNStatusId,
iNDocumentTypeID,
iNContractId,
#avz_gen_fnRetrieveActiveContract(iNMerchantID),
iNUploadUserId,
now()
);
end;
else
begin
update tb_documents 
set 
    FileName = iNFileName,
    FileDetails = iNFileDetails,
    ModifyUserId = iNUploadUserId,
    Modifydate = NOW(),
	StatusId=iNStatusId
where
    MerchantID = iNMerchantID
	and ContractId = iNContractId
	and DocumentId=iNDocumentId;    
END;
end if;

$$

DELIMITER ;

