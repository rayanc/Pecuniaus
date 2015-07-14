-- ================================================
-- Name: AVZ_col_spRetrieveCollectionDays
-- 
-- Description : To Retrieve contact data fro Preevention screen in collection
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 01-11-2014
-- ================================================
Drop procedure if exists AVZ_col_spRetrieveCollectionDays;

DELIMITER $$
CREATE PROCEDURE AVZ_col_spRetrieveCollectionDays
(iNDays int,
iNAssignedUserId bigint
)
BEGIN

if (iNDays =0) then
begin
select distinct
	contr.contractId,    
    m.merchantId,
	m.BusinessName as merchantName,   
	contr.ownedAmount,
    (contr.ownedAmount - IFNULL(contr.Paidamount, 0)) as pendingAmount,
    contr.Turn as expectedTurn,
    contr.RealTurn as realTurn,
	contr.retention,
	m.avgmccv as averagerccsalesCurrent,
	contr.CCAverageOffer as averageccsalesOffered,
	mrr.DaysWithoutActivity as daysWithoutActivity,
	IF( (count(cc.collectionId)/TIMESTAMPDIFF(MONTH,now(),contr.fundingDate))>=2, 1,0) as redColHighlight
from
	tb_contracts as contr     
	inner join tb_merchants m ON contr.MerchantID = m.MerchantId        
	inner join tb_merchantretrievalratio mrr on contr.ContractId = mrr.ContractId
	left join tb_collections cc on contr.ContractId = cc.ContractId    
	left join tb_creditcardactivity ccd on contr.ContractId = ccd.ContractId 
	where mrr.DaysWithoutActivity > 0
	-- and cc.AssignedUserID=iNAssignedUserId
	Group by m.merchantId
	order by mrr.DaysWithoutActivity desc; 
end;
else
begin
declare pDays varchar(100);

select SUBSTRING_INDEX(value, ' ',1) into pDays from tb_configuration where configid=iNDays and configsystem='COL';

select distinct
	contr.contractId,    
    m.merchantId,
	m.BusinessName as merchantName,   
	contr.ownedAmount,
    (contr.ownedAmount - IFNULL(contr.Paidamount, 0)) as pendingAmount,
    contr.Turn as expectedTurn,
    contr.RealTurn as realTurn,
	contr.retention,
	m.avgmccv as averagerccsalesCurrent,
	contr.CCAverageOffer as averageccsalesOffered,
	mrr.DaysWithoutActivity as daysWithoutActivity,
	IF( (count(cc.collectionId)/TIMESTAMPDIFF(MONTH,now(),contr.fundingDate))>=2, 1,0) as redColHighlight
from
	tb_contracts as contr     
    inner join tb_merchants as m ON contr.MerchantID = m.MerchantId
	left join tb_collections cc on contr.ContractId = cc.ContractId        
	left join tb_creditcardactivity ccd on contr.ContractId = ccd.ContractId 
	inner join tb_merchantretrievalratio mrr on contr.ContractId = mrr.ContractId   
    where mrr.DaysWithoutActivity between 1 and pDays
	-- and cc.AssignedUserID=iNAssignedUserId
	Group by m.merchantId
	order by mrr.DaysWithoutActivity desc;
end;
end if;

END$$
DELIMITER ;