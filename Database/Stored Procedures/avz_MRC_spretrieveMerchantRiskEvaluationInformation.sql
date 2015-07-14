
drop procedure if exists avz_MRC_spretrieveMerchantRiskEvaluationInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantRiskEvaluationInformation(
iNmerchantId bigint,
iNStartDate datetime,
iNEndDate datetime
)
begin

Select Distinct
CR.CreditReportID,CNT.ContractId,RE.merchantid MerchantId,TimeofReport EvaluationDate,CNT.ContractNumber,
CreditScore DateofCredit,RE.score ScoresGenerated
from tb_scorecard RE
inner join tb_creditreports CR on CR.ContractID=RE.contractid and CR.MerchantID=RE.merchantid
inner join tb_contracts CNT on CNT.ContractId=RE.contractid
Where RE.MerchantId=iNmerchantId and 
TimeofReport>=IF(iNStartDate='1900-01-01',TimeofReport,iNStartDate) and 
TimeofReport<=If(iNEndDate='1900-01-01',TimeofReport,iNEndDate);
end;