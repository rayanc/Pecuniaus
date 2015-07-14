drop procedure if exists avz_cc_spRetrieveSacanDocuments;

delimiter $$

create procedure avz_cc_spRetrieveSacanDocuments()
SELECT DISTINCT
    que.merchantId AS merchantId,
    doc.FileName
   FROM
    tb_ccvolumesqueue que
        JOIN
    tb_documents doc ON que.merchantId = doc.MerchantId
        JOIN
    lkp_tb_documenttypes doclist ON doc.DocumentTypeId = doclist.DocumentTypeId
    where doc.DocumentTypeId=11 and que.isprocessed is null;