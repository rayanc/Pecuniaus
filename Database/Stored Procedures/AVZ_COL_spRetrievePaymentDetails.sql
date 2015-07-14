-- ================================================
-- Name: AVZ_COL_spRetrievePaymentDetails
-- 
-- Description : To retrieve payment details
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spRetrievePaymentDetails;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spRetrievePaymentDetails
(iNMerchantId bigint,
iNContractId bigint
)
BEGIN

select dueDate,amount,lkp.statusName as status,collectiondate as dateOfPayment  
from tb_repaymentplan rp
left join lkp_tb_statuses lkp on rp.statusId=lkp.statusid
where rp.MerchantId=iNMerchantId and rp.ContractId=iNContractId and lkp.statusTypeId='COL' and lkp.IsActive=1;

end;
$$
DELIMITER ;