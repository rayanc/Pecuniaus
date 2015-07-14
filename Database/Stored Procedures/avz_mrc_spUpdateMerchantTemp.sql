/*call avz_mrc_spUpdateMerchantTemp( 138, 'Test Merchant','1/1/2001 12:00:00 AM', 'Test Merchant', 14009, 
11003, 12345678912, 5, 0, 1 ,1, merchantId); */

-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: comments before and after the routine body will not be stored by the server
-- --------------------------------------------------------------------------------
drop procedure if exists avz_mrc_spUpdateMerchantTemp;
DELIMITER $$

CREATE  PROCEDURE `avz_mrc_spUpdateMerchantTemp`(
iNmerchantId bigint,
iNbusinessName varchar(200),
iNbusinessStartDate date,
iNlegalName varchar(200),
iNindustryTypeid int,
iNbusinessTypeId int,
iNrnc varchar(20),
iNsalesRepId int,
iNmodifyuserId bigint,
iNisCompleted varchar(2),
iNisDuplicate varchar(2),
out merchantId bigint
)
begin 
declare pnewmerchantid bigint default 0;
declare _nMerchant bigint;


if (iNisDuplicate ='1') then
begin

update tb_merchants
set BusinessName=iNbusinessName,
BusinessStartDate=iNbusinessStartDate,
LegalName=iNlegalName,
IndustryTypeid=iNindustryTypeid,
BusinessTypeId=iNbusinessTypeId,
RNCNumber=iNrnc,
SalesRepId=iNsalesRepId,
ModifyDate=now(),
ModifyUserId=iNmodifyuserId
where  MerchantId = iNmerchantId;

call CreateDuplicateMReviewTask(1 ,22002 ,1 ,now() ,iNmerchantId ,null ,0 ,iNmerchantId ,1,iNmodifyuserId );

end;
else
begin

if exists (select merchantId from tmp_tb_merchants where MerchantId=iNmerchantId) then
begin

update tb_merchants
set BusinessName=iNbusinessName,
BusinessStartDate=iNbusinessStartDate,
LegalName=iNlegalName,
IndustryTypeid=iNindustryTypeid,
BusinessTypeId=iNbusinessTypeId,
RNCNumber=iNrnc,
SalesRepId=iNsalesRepId,
ModifyDate=now(),
ModifyUserId=iNmodifyuserId
where  MerchantId = iNmerchantId;

end;
else
begin

update tmp_tb_merchants
set businessName=iNbusinessName,
businessStartDate=iNbusinessStartDate,
legalName=iNlegalName,
industryTypeid=iNindustryTypeid,
businessTypeId=iNbusinessTypeId,
rncNumber=iNrnc,
salesRepId=iNsalesRepId,
modifydate=now(),
modifyuserId=iNmodifyuserId
where  merchantid_tmp = iNmerchantId;

end;
end if;
end;
end if;
if(iNisCompleted='1') then
begin
		call avz_mrc_spCompleteMerchantReviewTask(iNmerchantId,iNmodifyuserId,merchantId);       
end;
else
set merchantId=iNmerchantId;

end if;


end