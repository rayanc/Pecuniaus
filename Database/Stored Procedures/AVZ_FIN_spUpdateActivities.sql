-- ================================================
-- Name: AVZ_FIN_spUpdateActivities
-- 
-- Description : To Update activity in finance screen
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 15-10-2014
-- ================================================
Drop procedure if exists  AVZ_FIN_spUpdateActivities;
DELIMITER //
CREATE PROCEDURE AVZ_FIN_spUpdateActivities 
(iNMerchantId bigint,
iNActivityTypeId int,
iNDateOfActivity date,
iNAmount double, 
iNProcessorId int,
iNNotes varchar(2000),
iNInsertUserId bigint,
iNContractId bigint,
iNTransferContractId bigint)

BEGIN
declare cntactivityId int;
declare pActivityId bigint;
declare pOwnedAmount double default 0.0;
declare pLoanAmount double default 0.0;
declare pPaidAmount double default 0.0;
declare pPricePct double default 0.0;
declare pCapitalPct double default 0.0;

Select OwnedAmount,LoanedAmount,((LoanedAmount/OwnedAmount)), (((OwnedAmount-LoanedAmount)/OwnedAmount)),PaidAmount 
into pOwnedAmount,pLoanAmount,pCapitalPct,pPricePct,pPaidAmount
from tb_contracts where contractId=iNContractId;

if(iNActivityTypeId!=4) then
set iNAmount=-1*iNAmount;
end if;

INSERT INTO `tb_processoractivities`
(`MerchantId`,
`ProcessorId`,
`TotalAmount`,
`AppliedAmount`,
`Price`,
`Capital`,
`ActivityTypeId`,
`ProcessedDate`,
`Notes`,
`InsertUserId`,
`InsertDate`,
`ContractId`)
VALUES
(iNMerchantId,
iNProcessorId,
0,
iNAmount,
(pPricePct * iNAmount),
(pCapitalPct * iNAmount),
iNActivityTypeId,
iNDateOfActivity,
iNNotes,
iNInsertUserId,
now(),
iNContractId
);



SELECT LAST_INSERT_ID() into pActivityId;

Update tb_contracts set 
paidAmount= ifnull(paidAmount,0)+(-1*iNAmount), 
statusid=case when (ownedamount-paidAmount+iNAmount)<=0 then 20008
else
statusid
end
where contractId=iNContractId;

if(iNTransferContractId > 0) then
Select max(activityid) into cntactivityId from tb_processoractivities where contractId=iNTransferContractId;

Update tb_contracts set 
paidAmount= ifnull(paidAmount,0)-(-1*iNAmount), 
statusid=case when (ownedamount-paidAmount-iNAmount)>=0 then 20007
else statusid end where contractId=iNTransferContractId; 

update tb_processoractivities set AppliedAmount=AppliedAmount-(-1*iNAmount)
Where 
ActivityId=cntactivityId;

end if;

Select 
    pra.processedDate as dateOfActivity, ltp.Name as processorName, pra.AppliedAmount as totalAmount,
	act.ActivityName as activityType, pra.notes
from
    tb_processoractivities pra
	left join lkp_tb_processorlist ltp on pra.ProcessorId=ltp.ProcessorId
	left join lkp_tb_financeactivitylist act on pra.ActivityTypeId=act.ActivityId
	where ltp.IsActive=1 and pra.ActivityId=pActivityId;

END;
//
DELIMITER ;
