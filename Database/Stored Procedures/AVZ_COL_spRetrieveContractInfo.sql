-- ================================================
-- Name: AVZ_COL_spRetrieveContractInfo
-- 
-- Description : To Retrieve contact information
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 15-10-2014
-- ================================================
Drop procedure if exists AVZ_COL_spRetrieveContractInfo;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spRetrieveContractInfo
(iNMerchantId bigint,
iNContractId bigint)
BEGIN
Select sts.StatusName as collectionStatus, iNMerchantId as merchantId, iNContractId as contractId, ct.fundingDate,
if (IFNULL(ccd.totalAmount,0), 0, Sum(ccd.totalAmount) / 12) as monthlyAverage,
ct.CCAverageOffer, ct.LoanedAmount as AEC,
ct.ownedAmount, ct.retention, ct.turn as time,
(ct.ownedAmount- IFNULL(ct.Paidamount,0)) as pendingAmount
from
    tb_contracts ct
	left join tb_collections col on ct.ContractId= col.ContractId
	left join lkp_tb_statuses sts on col.StatusID=sts.StatusID
	left join tb_owners ow on ct.MerchantId=ow.MerchantId
	left join tb_creditcardactivity ccd on ct.ContractId = ccd.ContractId
	left join tb_contacts co on ow.ContactId = co.ContactId
    left join tb_merchants m on ct.MerchantId= m.MerchantId
	left join tb_offers off on ct.ContractId= off.ContractId
where    
	ct.MerchantId=iNMerchantId and ct.ContractId=iNContractId and ow.IsAuthorized=1 and sts.StatusTypeId = 'COL';

END$$
DELIMITER ;

