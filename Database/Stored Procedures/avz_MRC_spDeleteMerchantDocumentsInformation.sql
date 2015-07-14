drop procedure if exists avz_MRC_spDeleteMerchantDocumentsInformation;

delimiter $$

create procedure avz_MRC_spDeleteMerchantDocumentsInformation
(
iNdocumentId bigint
)
begin

delete from tb_documents where documentId=iNdocumentId;

end;