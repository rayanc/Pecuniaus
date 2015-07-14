drop procedure if exists avz_col_spRetrieveCollections;

delimiter $$
create procedure avz_col_spRetrieveCollections()
begin


SELECT 
    cnt.merchantId,
    cnt.contractid,
      col.collectionID ,
    expectedturn,
    realturn,
    collectionId,
    cnt.fundingDate,
  ( cnt.OwnedAmount- cnt.paidamount) as pendingamount
FROM
    tb_collections col
      right  JOIN
    tb_collectioncontracts collContracts ON collContracts.contractid = col.contractid
      right  JOIN
    tb_contracts cnt ON cnt.ContractId = col.ContractID;


end

