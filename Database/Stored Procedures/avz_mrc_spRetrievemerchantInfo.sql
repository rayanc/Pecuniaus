
drop procedure if exists `avz_mrc_spRetrievemerchantInfo`;
DELIMITER $$

CREATE  PROCEDURE `avz_mrc_spRetrievemerchantInfo`(
iNmerchantId bigint,  
iNtasktypeId bigint,
iNcontractId bigint
)
begin
declare searchstring longtext;
declare conditions longtext;

if exists(select MerchantId from tmp_tb_merchants  where MerchantId=iNmerchantId)  then
begin

set @searchstring='SELECT 
   merchantId,
	LegalName AS merchantName,
   LegalName AS legalName,
    BusinessName AS businessName,
    BusinessStartDate as businessStartDate,
    ifnull(BusinessTypeId,0) as BusinessTypeId,rncnumber as rnc,businessUrl as businessWebSite, assignedSalesRep,
	ownerName,
	ifnull(industryTypeId,0) as industryTypeId,
	SSN,
	phoneNumber,
	TypeofBusinessentity,
	address,
	ifnull(salesRepId,0) as salesRepId,
	ifnull(taskTypeId,0) as taskTypeId,
	tasKStatus,
	ifnull(taskStatusId,0) as taskStatusId,
	ifnull(merchantStatusId,0) as merchantStatusId,
	ifnull(contractStatusId,0) as contractStatusId,
	merchantStatus,
	contractStatus,
	ifnull(insertUserId,0) as insertUserId,
	Case contractStatusId
		when 20007 then
		0
		when 21002 then
		0
		else
		contractid
		end as contractid,
	 Case contractStatusId
		when 20007 then
		0
		when 21002 then
		0
		else
        ifnull(owedAmount,0) end as owedAmount,
	Case contractStatusId
		when 20007 then
		0
		when 21002 then
		0        
		else 
        ifnull(paidAmount,0) end as paidAmount,
	Case contractStatusId
		when 20007 then
		0
		when 21002 then
		0
		else
        (ifnull(owedAmount,0) - ifnull(paidAmount,0)) end as balanceAmount,
	UserName,
	IndustryType,
	propertytype

 
FROM avz_vw_merchantdetails ';

if(iNmerchantId!=0) then 
 set @searchstring=  concat(@searchstring,'  where merchantId =',iNmerchantId ); 
 END IF;
 end;

if(iNcontractId!=0) then 
 set @searchstring=  concat(@searchstring,'  and contractid =',iNcontractId); 
 else
 set @searchstring=  concat(@searchstring,'  and (contractid = AVZ_GEN_FNRETRIEVEACTIVECONTRACT(',iNmerchantId,') or ifnull(contractid,0)=0)'); 
 END IF;
else
begin

set @searchstring='SELECT 
   merchantId,
   LegalName AS legalName,
   LegalName AS merchantName,
    BusinessName AS businessName,
    BusinessStartDate as businessStartDate,
   ifnull(BusinessTypeId,0) as BusinessTypeId,
    rncnumber as rnc,
    businessUrl as businessWebSite,
    assignedSalesRep,
	ownerName,
	ifnull(industryTypeId,0) as industryTypeId,
	SSN,
	phoneNumber,
	TypeofBusinessentity,
	"" as address,
	ifnull(salesRepId,0) as salesRepId,
	ifnull(taskTypeId,0) as taskTypeId,
	tasKStatus,
	ifnull(taskStatusId,0) as taskStatusId,
	ifnull(merchantStatusId,0) as merchantStatusId,
	ifnull(contractStatusId,0) as contractStatusId,
	merchantStatus,
	contractStatus,
	ifnull(insertUserId,0) as insertUserId,
	0 as contractid,
	0 as owedAmount,0 as paidAmount,0 as balanceAmount,
	UserName,
	IndustryType,
	propertytype
FROM avz_vw_merchanttempdetails';
end;

if(iNmerchantId!=0) then 
 set @searchstring=  concat(@searchstring,'  where merchantId =',iNmerchantId ); 
 END IF;
end if;
 

if(iNtasktypeId!=0) then 
begin
 set @searchstring=  concat(@searchstring,'  and taskTypeId =',iNtasktypeId, ' and taskStatusId in(22001,22002,22004) ORDER BY TaskId desc LIMIT 1 ');
end;
else
begin
	set @searchstring=  concat(@searchstring,' and taskStatusId in(22001,22002,22004) ORDER BY TaskId desc LIMIT 1 ');
end;
end if;


 PREPARE stmt FROM @searchstring; 
 EXECUTE stmt;
 DEALLOCATE PREPARE stmt;

end





