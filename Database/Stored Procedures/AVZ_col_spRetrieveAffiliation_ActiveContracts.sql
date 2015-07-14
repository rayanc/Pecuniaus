-- ================================================
-- Name: AVZ_col_spRetrieveAffiliation_ActiveContracts
-- 
-- Description : To retrive active contracts related to affiliations
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
drop procedure if exists AVZ_col_spRetrieveAffiliation_ActiveContracts;

DELIMITER $$
CREATE PROCEDURE AVZ_col_spRetrieveAffiliation_ActiveContracts
(iNMerchantId bigint,
iNIsRNC bit
)
BEGIN	
if(iNIsRNC=1) then
begin

declare pRNCNumber varchar(11);
select RNCNumber into pRNCNumber from tb_merchantaffiliations where MerchantId=iNMerchantId;
select 
    ma.affiliationId,
    m.BusinessName as merchantName,
    ma.merchantId,
    ct.LoanedAmount as aecAmount,
    ct.ownedAmount,
    (ct.ownedAmount - IFNULL(ct.Paidamount, 0)) as pendingAmount,
    ct.Retention as retentionTime,
    ct.fundingDate,
    stats.StatusName as merchantStatus
from
    tb_merchantaffiliations ma
        left join
    tb_merchants m ON ma.MerchantId = m.MerchantId
	left join
   tb_contracts ct ON ct.ContractId = avz_gen_fnRetrieveActiveContract(ma.MerchantId)
        left join
    lkp_tb_statuses as stats ON m.StatusId = stats.StatusId and stats.StatusTypeId='MRC' and stats.isActive=1
where
     ma.RNCNumber=pRNCNumber and ma.MerchantId <> iNMerchantId
        and ma.OwnerId is null ;
end;
else
begin
declare pOwnerId bigint;
select OwnerId into pOwnerId from tb_merchantaffiliations where MerchantId=iNMerchantId;
select 
    ma.affiliationId,
    m.BusinessName as merchantName,
    ma.merchantId,
    ct.LoanedAmount as aecAmount,
    ct.ownedAmount,
    (ct.ownedAmount - IFNULL(ct.Paidamount, 0)) as pendingAmount,
    ct.Retention as retentionTime,
    ct.fundingDate,
    stats.StatusName as merchantStatus
from
    tb_merchantaffiliations ma
        left join
    tb_merchants m ON ma.MerchantId = m.MerchantId
	left join
   tb_contracts ct ON ct.ContractId = avz_gen_fnRetrieveActiveContract(ma.MerchantId)
        left join
    lkp_tb_statuses as stats ON m.StatusId = stats.StatusId and stats.StatusTypeId='MRC' and stats.isActive=1
where
     ma.OwnerId=pOwnerId and ma.MerchantId <> iNMerchantId
        and ma.RNCNumber is null ;
end;
end if;
end