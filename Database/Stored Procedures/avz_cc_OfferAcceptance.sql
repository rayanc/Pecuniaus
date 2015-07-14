drop procedure if exists avz_cc_OfferAcceptance;
delimiter $$

create procedure avz_cc_OfferAcceptance
(
iNdataxml text,
iNmerchantid bigint,
iNcontractid bigint

)
begin
declare pRowIndex int unsigned default 1;   
declare pRowCount int unsigned default 0;
declare pofferId int unsigned default 0;
declare pofferstatusid int unsigned default 0; 	

set pRowCount  = extractValue(iNdataxml, 'count(//offers)');
    
set pofferId=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@offerId'),''),0);
if(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'merchantinfo/offers/@IsSelected'),''),0)="True") then
	set pofferstatusid=190001;
else
	set pofferstatusid=190002;
end if;

update tb_offers set status=190002 where MerchantId=iNmerchantid and ContractId=iNcontractid;
set pofferstatusid=190001;
update tb_offers
set 

Turn=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@turn'),''),0),
LoanAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@loanAmount'),''),0),
OwnedAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@ownedAmount'),''),0),
Proportion=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@proportion'),''),0),
Retention=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@retention'),''),0),
ModifyDate= Date_format(now(),'%y-%m-%d'),
OfferAcceptanceDate=Date_format(now(),'%y-%m-%d'),
ModifyUserId=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@insertuserId'),''),0),
monthlypayment=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@monthlyPayment'),''),0),
salestaken=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@salestaken'),''),0),
yearly=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@yearly'),''),0),
isynced=0,
status=pofferstatusid
where OfferId =pofferId;


update tb_contracts set 
LoanedAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@loanAmount'),''),0)
, ownedAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@ownedAmount'),''),0)
,Retention=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@retention'),''),0)
, Turn=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/offers/@turn'),''),0)
, CCAverageOffer= (select avgmccv from tb_merchants where merchantId=iNmerchantid limit 1)
, AverageMCCV =(select avgmccv from tb_merchants where merchantId=iNmerchantid limit 1)

where contractId =iNcontractid;




end;


