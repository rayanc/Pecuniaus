-- ================================================
-- Name: AVZ_col_spRetrieveCollectionForPrevention
-- 
-- Description : To Retrieve contact data fro Preevention screen in collection
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 01-11-2014
-- ================================================
Drop procedure if exists AVZ_col_spRetrieveCollectionForPrevention;

DELIMITER $$
CREATE PROCEDURE AVZ_col_spRetrieveCollectionForPrevention
(iNSlowPercentage decimal,
iNAssignedUserId bigint
)
BEGIN

if (iNSlowPercentage =0) then
begin
select distinct
    contr.contractId,
    m.merchantId,
    m.BusinessName as merchantName,
    (contr.ownedAmount - IFNULL(contr.Paidamount, 0)) as pendingAmount,
    contr.fundingDate,
    contr.Turn as expectedTurn,
    contr.RealTurn as realTurn,
	abs(mrr.RetrievalSpeed) as percentage
from
    tb_contracts as contr
        inner join
    tb_merchants as m ON contr.MerchantID = m.MerchantId
        join
    tb_merchantretrievalratio as mrr ON contr.ContractID = mrr.ContractID
	left join tb_collections as col on contr.contractId=col.ContractID
	where mrr.RetrievalSpeed < 0
	-- and col.AssignedUserID=iNAssignedUserId
    Group by m.merchantId
	order by mrr.RetrievalSpeed desc;
end;
else
begin
select distinct
    contr.contractId,
    m.merchantId,
    m.BusinessName as merchantName,
    (contr.ownedAmount - IFNULL(contr.Paidamount, 0)) as pendingAmount,
    contr.fundingDate,
    contr.Turn as expectedTurn,
    contr.RealTurn as realTurn,
	abs(mrr.RetrievalSpeed) as percentage
from
    tb_contracts as contr
        inner join
    tb_merchants as m ON contr.MerchantID = m.MerchantId
        join
    tb_merchantretrievalratio as mrr ON contr.ContractID = mrr.ContractID 
	left join tb_collections as col on contr.contractId=col.ContractID
	where mrr.RetrievalSpeed < 0 and
		abs(mrr.RetrievalSpeed) between 1 and iNSlowPercentage
	-- and col.AssignedUserID=iNAssignedUserId
    Group by m.merchantId
	order by mrr.RetrievalSpeed desc;
end; 
end if;
END$$
DELIMITER ;