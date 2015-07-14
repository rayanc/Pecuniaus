-- ================================================
-- Name: AVZ_COL_spRetrieveNonActiveContracts_Affiliation
-- 
-- Description : To retrive non active contracts related to affiliations
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
drop procedure if exists AVZ_COL_spRetrieveNonActiveContracts_Affiliation;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spRetrieveNonActiveContracts_Affiliation
(iNMerchantId bigint,
iNIsRNC bit
)
BEGIN	
if(iNIsRNC=1) then
begin

declare pRNCNumber varchar(11);
select RNCNumber into pRNCNumber from tb_merchantaffiliations where MerchantId=iNMerchantId;
select 
	ma.merchantId,
    ma.affiliationId,
    m.BusinessName as merchantName,
    ct.CreationDate as creationDate, 
	sc.dateevaluated as lastDateofEvaluation,
	m.avgmccv as requestedCCVolumes, 
     CASE
        WHEN ccq.isprocessed = 1 THEN 'Sent'
        ELSE 'Not Sent'
    END AS volumeStatus 
from
    tb_merchantaffiliations ma
        left join
    tb_merchants m ON ma.MerchantId = m.MerchantId
	left join
   tb_contracts ct ON ma.MerchantID = ct.MerchantId
        left join
    lkp_tb_statuses as stats ON m.StatusId = stats.StatusId and stats.StatusTypeId='MRC' and stats.isActive=1
	left join
   tb_scorecard sc ON ct.ContractId = sc.ContractId
left join
   tb_ccvolumesqueue ccq ON ct.MerchantId = ccq.MerchantId
where
     ma.RNCNumber=pRNCNumber and ma.MerchantId <> iNMerchantId
        and ma.OwnerId is null and ct.StatusId<> 20007;
end;
else
begin
declare pOwnerId bigint;
select OwnerId into pOwnerId from tb_merchantaffiliations where MerchantId=iNMerchantId;
select 
	ma.merchantId,
    ma.affiliationId,
    m.BusinessName as merchantName,
    ct.CreationDate as creationDate, 
	sc.dateevaluated as lastDateofEvaluation,
	m.avgmccv as requestedCCVolumes, 
     CASE
        WHEN ccq.isprocessed = 1 THEN 'Sent'
        ELSE 'Not Sent'
    END AS volumeStatus 
from
    tb_merchantaffiliations ma
        left join
    tb_merchants m ON ma.MerchantId = m.MerchantId
	left join
   tb_contracts ct ON ma.MerchantID = ct.MerchantId
        left join
    lkp_tb_statuses as stats ON m.StatusId = stats.StatusId and stats.StatusTypeId='MRC' and stats.isActive=1
left join
   tb_scorecard sc ON ct.ContractId = sc.ContractId
left join
   tb_ccvolumesqueue ccq ON ct.MerchantId = ccq.MerchantId
where
     ma.OwnerId=pOwnerId and ma.MerchantId <> iNMerchantId
        and ma.RNCNumber is null and ct.StatusId<> 20007;
end;
end if;
end