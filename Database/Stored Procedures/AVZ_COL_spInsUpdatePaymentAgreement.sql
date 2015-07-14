-- ================================================
-- Name: AVZ_COL_spInsUpdatePaymentAgreement
-- 
-- Description : To insert or update the payment agreement
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================

#call AVZ_COL_spInsUpdatePaymentAgreement(129, 34,'2015-04-28','2015-05-28','2016-04-28',15,2);
Drop procedure if exists AVZ_COL_spInsUpdatePaymentAgreement;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spInsUpdatePaymentAgreement
(iNMerchantId bigint,
iNContractId bigint,
iNDateOfAgreement datetime,
iNStartDate datetime,
iNEndDate datetime,
iNIntervalofDays int,
iNInsertUserId bigint
)
BEGIN

Declare pPayments int;
Declare pAmount double;

INSERT INTO `tb_paymentagreement`
(`DateOfAgreement`,
`StartDate`,
`EndDate`,
`DaysOfInterval`,
`MerchantId`,
`ContractId`,
`InsertDate`,
`InsertUserId`)
VALUES
(iNDateOfAgreement,
iNStartDate,
iNEndDate,
iNIntervalofDays,
iNMerchantId,
iNContractId,
now(),
iNInsertUserId);

Set pPayments= CEIL(DateDiff(iNEndDate,iNStartDate)/iNIntervalofDays);
Set pAmount=(Select ownedAmount-paidAmount from tb_contracts where contractid=iNContractId)/pPayments;

While (pPayments > 0) DO

Insert into tb_repaymentplan( merchantId,ContractID,CompanyId, Amount,Collectionfrequency,Duedate,statusid,insertUserId,insertdate)
Select  iNMerchantId, iNContractId,1,pAmount,iNIntervalofDays,DATE_ADD(iNStartDate, interval (pPayments*iNIntervalofDays) DAY),180006,2,now();

set pPayments=pPayments-1;
END WHILE ;
end;
$$
DELIMITER ;