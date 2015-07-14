
drop procedure if exists `avz_mrc_spRetrievemerchantSearch`;
DELIMITER $$

CREATE  PROCEDURE `avz_mrc_spRetrievemerchantSearch`(
iNmerchantId bigint  ,
iNcontractId bigint,
iNtaskType varchar(200),
iNworkflowId bigint,
instatusId bigint,
iNrnc varchar(200),
iNlegalName varchar(200),
iNbusinessName varchar(200),
iNprocessornbr bigint,
iNprocessorName varchar(200),
iNownerName varchar(200)
)
begin
declare searchstring longtext;
declare conditions longtext;
declare  searchstringTemp longtext;
set @searchstring='SELECT 
   merchantId,
   LegalName AS legalName,
    BusinessName AS businessName,
    BusinessStartDate as businessStartDate,
    BusinessTypeId,
    rncnumber as rnc,
    businessUrl as businessWebSite,
   assignedSalesRep,
ownerName,
industryTypeId,
SSN,
phoneNumber,
TypeofBusinessentity,
   0 as isTemp
FROM
avz_vw_merchantdetails where 1=1'; 
set @searchstringTemp='
SELECT 
    merchantId,
    LegalName AS legalName,
    BusinessName AS businessName,
    BusinessStartDate as businessStartDate,
    BusinessTypeId,
    rncnumber as rnc,
    businessUrl as businessWebSite,   
assignedSalesRep,
ownerName,
industryTypeId,
SSN,
phoneNumber,
TypeofBusinessentity,
   1 as isTemp
FROM
avz_vw_merchantTempdetails where 1=1

 ';

 if(iNmerchantId!=0) then 
 set @searchstring=  concat(@searchstring,'  and merchantid =',iNmerchantId);
 set @searchstringTemp=  concat(@searchstring,'  and merchantid =',iNmerchantId);
 END IF;
 if (iNcontractId!=0)
 then
 set @searchstring=  concat(@searchstring,' and contractId =  ',iNcontractId);
 set @searchstringTemp=  concat(@searchstring,' and contractId =  ',iNcontractId);
 end if;
 if(iNworkflowId!=0) then
 
 set @searchstring=  concat(@searchstring,'and workflowId =  ',iNworkflowId);
  set @searchstringTemp=  concat(@searchstring,'and workflowId =  ',iNworkflowId);
 end if;
 
 if(iNlegalName!='') then
set @searchstring=  concat(@searchstring,'  and', ' legalName ', ' like ','''%', iNlegalName,'%''');
set @searchstringTemp=  concat(@searchstring,'  and', ' legalName ', ' like ','''%', iNlegalName,'%''');
end if;

 if(iNbusinessName!='') then
 set @searchstring=  concat(@searchstring,'  and', ' businessName ', ' like ','''%', iNbusinessName,'%''');
 set @searchstringTemp=  concat(@searchstring,'  and', ' businessName ', ' like ','''%', iNbusinessName,'%''');
 end if;
 
 if(iNrnc!='') then
 
 set @searchstring=  concat(@searchstring,'  and', ' rncnumber ', ' like ','''%', iNrnc,'%''');
  set @searchstringTemp=  concat(@searchstring,'  and', ' rncnumber ', ' like ','''%', iNrnc,'%''');
  end if;

 set @conditions=concat(@searchstring,'  union   ',@searchstringTemp);
 PREPARE stmt FROM @conditions;
 
 EXECUTE stmt;
 DEALLOCATE PREPARE stmt;

end




