drop view if exists Pecuniaus.avz_vw_ActiveAffiliations;


DELIMITER $$

create view Pecuniaus.avz_vw_ActiveAffiliations
as
select coll.collectionId, contr.contractId,m.LegalName as merchantName,m.merchantId,m.RNCNumber, (contr.ownedAmount- IFNULL(contr.Paidamount,0)) as pendingAmount
,contr.ownedAmount
, contr.LoanedAmount as aecAmount
, '45' as retentionTime
,contr.fundingDate
,stats.StatusName as merchantStatus
,m.OwnerId
FROM  Pecuniaus.tb_merchants  as m

inner join Pecuniaus.tb_collections as coll on coll.MerchantID=m.MerchantId
inner join Pecuniaus.tb_contracts as contr on contr.ContractId=coll.ContractID 
and m.MerchantId=contr.MerchantId
inner join Pecuniaus.lkp_tb_statuses as stats on stats.StatusId=m.StatusId 
and stats.IsActive=1 and contr.statusID=20007;



