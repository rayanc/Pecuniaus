
drop procedure if exists avz_cc_spRetrieveCreditReports;
delimiter $$

create procedure avz_cc_spRetrieveCreditReports(iNmerchantId bigint, iNcontractId bigint)

begin 
select creditReportId,creditscore,
firstName,
lastName,
name,
middleName,
rawData as rawData ,
probabilityOfDefault,
DATE_FORMAT(TimeofReport, '%Y-%m-%d %T') as timeofreport,
#monthEvaualted,
merchantId,
contractId,
occupation,
nationality,
type,
numberofLoans,
numberofOthers,
numberofCreditCards,
isavailable

from  tb_creditreports where merchantId=iNmerchantId and contractId=iNcontractId
order by timeofreport desc, CreditReportID desc ;
end;