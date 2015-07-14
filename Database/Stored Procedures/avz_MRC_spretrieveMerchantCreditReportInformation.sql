
drop procedure if exists avz_MRC_spretrieveMerchantCreditReportInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantCreditReportInformation(iNReportID bigint)
begin
Select 
count(numberofaccounts) NumberofLoans,approvedcredit ApprovedAmount,owedamount OwnedAmount,monthlypayment MonthlyPayment,lateamount LateAmount,
amountinlegal AmountInLegal,'0'LateIndex,'0' DebtIndex,CRA.Type AnalysisType,CR.Type ReportType
from tb_creditreports CR
Inner Join tb_creditreportAnalysis CRA on CRA.reportId=CR.CreditReportID
Where CRA.reportId=iNReportID
group by CRA.type;
/*if(iNContractID=0) then
Select 
numberofaccounts NumberOfAccount,approvedcredit ApprovedCredit,owedamount OwnedAmount,monthlypayment MonthlyPayment,lateamount LateAmount,
amountinlegal AmountInLegal,'0'LateIndex,'0' DebtIndex,CRA.Type AnalysisType,CR.Type ReportType
from tb_creditreports CR
Inner Join tb_creditreportAnalysis CRA on CRA.reportId=CR.CreditReportID
Where CR.MerchantID=iNmerchantId;
elseif(iNContractID>0) then
Select 
numberofaccounts NumberOfAccount,approvedcredit ApprovedCredit,owedamount OwnedAmount,monthlypayment MonthlyPayment,lateamount LateAmount,
amountinlegal AmountInLegal,'0'LateIndex,'0' DebtIndex,CRA.Type AnalysisType,CR.Type ReportType
from tb_creditreports CR
Inner Join tb_creditreportAnalysis CRA on CRA.reportId=CR.CreditReportID
Where CR.ContractID=iNContractID;
end if;*/
end;