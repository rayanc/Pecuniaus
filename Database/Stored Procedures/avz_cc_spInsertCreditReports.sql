drop procedure if exists avz_cc_spInsertCreditReports;
delimiter $$

create procedure avz_cc_spInsertCreditReports(
 iNmerchantId bigint,
 iNcontractId bigint,
 iNdataxml text,
 iNimage text,
 iNrawData longtext,
iNisavailable smallint
 )
 begin
	declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned default 0;
    declare pcreditReportId int unsigned default 0;	
 
	
    set pcreditReportId= COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@creditReportId'),''),0);
    
    if exists(select 1 from tb_creditreports where creditReportId=pcreditReportId)=0 then 
    insert into tb_creditreports 
( 
creditscore,
firstName,
lastName,
name,
middleName,
rawData,
probabilityOfDefault,
monthEvaualted,
merchantId,
contractId,
Occupation,
nationality,
commercialname,
commercialactivity,
rnc,
timeofreport,
NumberofCreditCards,
Errors,
type,
image,
numberofLoans,
numberofOthers,
isavailable
)

values(
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@creditscore'),''),0.00),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@firstName'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@lastName'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@name'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@middleName'),''),''),
iNrawData,
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@probabilityOfDefault'),''),0),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@monthEvaualted'),''),0),
iNmerchantId,
iNcontractId,
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@occupation'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@nationality'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@commercialname'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@commercialactivity'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@rnc'),''),''),
date_format(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@timeofreport'),''),'01-01-1900'),'%y-%m-%d'),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@numberofCreditCards'),''),0),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@errors'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@type'),''),'O'),
iNimage,
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@numberofLoans'),''),0),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@numberofOthers'),''),0),
iNisavailable
);


set pcreditReportId= last_insert_id();
 

#Navigate through the list of the data priovided from the front end to insert update


else
set sql_safe_updates=0;
 update tb_creditreports set
creditscore=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@creditscore'),''),0),
firstName=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@firstName'),''),0),
lastName=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@lastName'),''),0),
name=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@name'),''),0),
middleName=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@middleName'),''),0),
rawData=iNrawData,
probabilityOfDefault=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@probabilityOfDefault'),''),0),
monthEvaualted=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@monthEvaualted'),''),0),
merchantId=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@merchantId'),''),0),
contractId=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@contractId'),''),0),
Occupation=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@Occupation'),''),0),
nationality=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@nationality'),''),0),
commercialname=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@commercialname'),''),0),
commercialactivity=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@commercialactivity'),''),0),
rnc=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@rnc'),''),0),
numberofLoans =COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@numberofLoans'),''),0),
numberofOthers=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@numberofOthers'),''),0)

where creditReportId=pcreditReportId;
 end if;
set pRowCount  = extractValue(iNdataxml, 'count(//creditanalysis)');
   
set pRowIndex=0;
Select pRowCount;

    while pRowIndex <= pRowCount do
#Select nullif(REPLACE(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@monthconsolidated'),"'",''), '');
call avz_cc_spInsertCreditAnalaysis(pcreditReportId,
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@numberofloans'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@loancredit'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@loanowedamount'), ''),0),
nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@firstcreditdate'), ''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@approvedcredit'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@loanamountinlegal'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@loanlateamount'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@loanmonthlypayment'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@typeofActivity'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@currency'), ''),0),
#Select nullif(REPLACE(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@monthconsolidated'),"'",''), '')
nullif(REPLACE(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@monthconsolidated'),"'",''), '')
);

	set pRowIndex = pRowIndex + 1;
			 end while;
end;