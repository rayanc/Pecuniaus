

drop procedure if exists avz_MRC_spretrieveMerchantContractInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantContractInformation(
iNmerchantId bigint,
iNStartDate datetime,
iNEndDate datetime
)
begin

Select distinct
CNT.contractId as ContractId,ContractNumber,case when fundingDate="1/1/0001" then null else fundingdate end FundingDate,CNT.LoanedAmount,CNT.OwnedAmount,
CNT.OwnedAmount-CNT.LoanedAmount, OFF.Proportion Price,OFF.Retention RetentionPercentage, CAST(CNT.Turn AS DECIMAL(10,2)) Time,CAST(CNT.RealTurn AS DECIMAL(10,2)) RealTime
,CreationDate, ifnull(paidAmount,0) PaidAmount,CNT.OwnedAmount-ifnull(paidAmount,0) PendingAmount,StatusName ContractStatus,
If((PaidAmount/CNT.OwnedAmount)/100 Is Null,0,(PaidAmount/CNT.OwnedAmount)/100) PaidPercentage,ifnull(score,'--') Score
from  tb_contracts CNT
left join tb_offers OFF on OFF.ContractId=CNT.ContractId and OFF.status=190001
inner join lkp_tb_statuses ST on CNT.StatusId=ST.StatusId
left join tb_scorecard SC on SC.contractid=CNT.contractId and SC.scorecardid=(select max(s.scorecardid) from tb_scorecard s where s.contractid=CNT.contractId)
Where CNT.MerchantId=iNmerchantId; 

/*Select distinct
CNT.contractId as ContractId,ContractNumber,fundingDate FundingDate,LoanedAmount,OwnedAmount,OwnedAmount-LoanedAmount Price,Retention RetentionPercentage, CNT.Turn Time,cnt.RealTurn RealTime,CreationDate,
ifnull(paidAmount,0) PaidAmount,OwnedAmount-ifnull(paidAmount,0) PendingAmount,StatusName ContractStatus,
If((PaidAmount/OwnedAmount)/100 Is Null,0,(PaidAmount/OwnedAmount)/100) PaidPercentage,ifnull(score,'--') Score
from  tb_contracts CNT
inner join lkp_tb_statuses ST on CNT.StatusId=ST.StatusId
left join tb_scorecard SC on SC.contractid=CNT.contractId and SC.scorecardid=(select max(s.scorecardid) from tb_scorecard s where s.contractid=CNT.contractId)
Where CNT.MerchantId=iNmerchantId; */
/*and CNT.CreationDate>=IF(iNStartDate='1900-01-01',CNT.FundingDate,iNStartDate) and 
CNT.CreationDate<=If(iNEndDate='1900-01-01',CNT.FundingDate,iNEndDate);*/

end;

select  CAST(Turn AS DECIMAL(10,2)),CAST(RealTurn AS DECIMAL(10,2)) from tb_contracts where merchantid=208;