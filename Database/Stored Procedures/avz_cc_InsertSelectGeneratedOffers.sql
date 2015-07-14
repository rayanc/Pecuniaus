drop procedure if exists avz_cc_InsertSelectGeneratedOffers;
delimiter $$

create procedure avz_cc_InsertSelectGeneratedOffers
(
iNdataxml text,
iNmerchantid bigint,
iNcontractid bigint,
iNannualSales double,
iNavgmccv double,
iNreason text,
iNismccvupdated smallint

)
begin
declare pRowIndex int unsigned default 1;   
declare pRowCount int unsigned default 0;
declare pofferId int unsigned default 0;
declare pofferstatusid int unsigned default 0; 	
declare offernote text;	

set pRowCount  = extractValue(iNdataxml, 'count(//offers)');
    
while pRowIndex <= pRowCount do
set pofferId=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@offerId'),''),0);

if(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@IsSelected'),''),0)="True") then
	set pofferstatusid=190001;
else
	set pofferstatusid=190002;
end if;

if (pofferId=0 ) then
INSERT INTO tb_offers
(

MerchantId,
ContractId,
Turn,
LoanAmount,
OwnedAmount,
Proportion,
Retention,
OfferCreationDate,
CreatedUserId,
expirationDate,
monthlypayment,
salestaken,
yearly,
status
)
VALUES
(
iNmerchantid,
iNcontractid,
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@turn'),''),0),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@loanAmount'),''),0),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@ownedAmount'),''),0),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@proportion'),''),0),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@retention'),''),0),
now(),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@insertuserId'),''),0),
DATE_ADD(now(),INTERVAL 30 day),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@monthlyPayment'),''),0),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@salestaken'),''),0),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@yearly'),''),0)
,pofferstatusid);
set pofferId= LAST_INSERT_ID();

set offernote= concat('Offer MCA Amount: ',COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@loanAmount'),''),0)
,' Offer Owned Amount: ', COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@ownedAmount'),''),0), 
'  Offer Price: ',COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@proportion'),''),0),
' Retention: ', COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@retention'),''),0),
'  Turn: ',COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@turn'),''),0) );

insert into tb_notes(NoteTypeId, merchantId, contractId, Note, WorkflowId, ScreenName, InsertDate)
values(550001,iNmerchantId,iNcontractid,offernote,1,'Offer Creation',date_format(now(),'%y-%m-%d'));


else

update tb_offers
set 

Turn=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@turn'),''),0),
LoanAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@loanAmount'),''),0),
OwnedAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@ownedAmount'),''),0),
Proportion=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@proportion'),''),0),
Retention=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@retention'),''),0),
ModifyDate=now(),
ModifyUserId=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@insertuserId'),''),0),
expirationDate=DATE_ADD(now(),INTERVAL 30 day),
monthlypayment=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@monthlyPayment'),''),0),
salestaken=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@salestaken'),''),0),
yearly=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@yearly'),''),0),
status=pofferstatusid
where OfferId =pofferId;

end if;


if(pofferstatusid=190001) then
begin

update tb_contracts set 
LoanedAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@loanAmount'),''),0)
, ownedAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@ownedAmount'),''),0)
, Turn=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@turn'),''),0)
, CCAverageOffer= (select avgmccv from tb_merchants where merchantId=iNmerchantId limit 1)
, AverageMCCV =(select avgmccv from tb_merchants where merchantId=iNmerchantId limit 1)

where contractId =iNcontractid;

update tb_offers
set OfferAcceptanceDate= date_format(now(), '%y-%m-%d')
where OfferId =pofferId;
end;
end if;

set pofferId=0;
set offernote='';
set pRowIndex = pRowIndex + 1;

end while;


call avz_mrc_spAddManualMCCV(iNmerchantId,iNannualSales,iNavgmccv,iNreason,iNcontractid);


end;


