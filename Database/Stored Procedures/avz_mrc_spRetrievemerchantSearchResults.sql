/*call avz_mrc_spRetrievemerchantSearchResults(0,0,0,1,0,'','','Testing',0,'','',0,'D',2);*/

drop procedure if exists `avz_mrc_spRetrievemerchantSearchResults`;
DELIMITER $$

CREATE  PROCEDURE `avz_mrc_spRetrievemerchantSearchResults`(
iNmerchantId bigint  ,
iNcontractId bigint,
iNtaskType bigint,
iNworkflowId bigint,
instatusId bigint,
iNrnc varchar(200),
iNlegalName varchar(200),
iNbusinessName varchar(200),
iNprocessornbr bigint,
iNprocessorName varchar(200),
iNownerName varchar(200),
iNshowTemp int,
iNSearchType varchar(200),
iNAssignedUserId bigint
)
begin
declare searchstring longtext;
declare searchTempstring longtext;
declare searchDuptring longtext;
declare conditions longtext;

declare adminGroup integer;

select GroupId into adminGroup from tb_usergroups where UserID=iNAssignedUserId;
set @searchDuptring='SELECT distinct
   merchantId,
   merchantName,
legalName,
RNCNumber as rnc,
BusinessName as businessName, 
ownerName,
industrytype,
0 as RentFlag

FROM
avz_vw_merchantSearchResults where 1=1'; 

set @searchstring='SELECT 
   merchantId,
   merchantName,
legalName,
RNCNumber as rnc,
BusinessName as businessName, 
ownerName,
assignedSalesRep,
salesRepId,
taskName as taskName,
WorkflowId,
contractid,
assigneduserId,
taskStatusId ,
tasKStatus ,
   0 as isTemp,
taskTypeId,
RentFlag,
industrytype,
UserName as assignedUser,
CompletionDate,
AssignedDate,
merchantStatus,
merchantStatusId
FROM
avz_vw_merchantSearchResults where 1=1'; 

set @searchTempstring='SELECT 
   merchantId,
   merchantName,
legalName,
RNCNumber as rnc,
BusinessName as businessName, 
ownerName,
assignedSalesRep,
salesRepId,
taskName as taskName,
WorkflowId,
contractid,
assigneduserId,
taskStatusId,
tasKStatus ,
   1 as isTemp,
taskTypeId,
0 as RentFlag,
industrytype,
UserName as assignedUser,
CompletionDate,
AssignedDate,
merchantStatus,
merchantStatusId
FROM
avz_vw_merchantTempSearchResults where 1=1'; 

if(adminGroup!=5) then
set @searchstring=  concat(@searchstring,'  and (',iNAssignedUserId,'=0 OR assigneduserId =',iNAssignedUserId,')');
set @searchTempstring=  concat(@searchTempstring,'  and (',iNAssignedUserId,'=0 OR assigneduserId =',iNAssignedUserId,')');
end if;

 if(iNmerchantId!=0) then 
 set @searchstring=  concat(@searchstring,'  and merchantid =',iNmerchantId);
 set @searchTempstring=  concat(@searchTempstring,'  and merchantid =',iNmerchantId);
 set @searchDuptring= concat(@searchDuptring,'  and merchantid =',iNmerchantId);
 END IF;
 if (iNcontractId!=0)
 then
 set @searchstring=  concat(@searchstring,' and contractId =  ',iNcontractId); 
 set @searchTempstring=  concat(@searchTempstring,'  and contractId =',iNcontractId);
 end if;

if(iNtaskType!=0) then
 
 set @searchstring=  concat(@searchstring,'  and taskTypeId = ', iNtaskType);  
set @searchTempstring=  concat(@searchTempstring,' and taskTypeId = ', iNtaskType);  
end if;

if(instatusId!=0) then
	if(iNSearchType ='MP') then
		 begin
			 set @searchstring=  concat(@searchstring,'  and merchantStatusId = ', instatusId);  
         end;
         else
         begin
			 set @searchstring=  concat(@searchstring,'  and taskStatusId = ', instatusId);  
			 set @searchTempstring=  concat(@searchTempstring,' and taskStatusId = ', instatusId);  
		end;
        end if;
 end if;
 
 if(iNlegalName!='') then
 set @searchstring=  concat(@searchstring,'  and', ' legalName ', ' like ','''%', iNlegalName,'%''');
 set @searchTempstring=  concat(@searchTempstring,'  and', ' legalName ', ' like ','''%', iNlegalName,'%''');
 set @searchDuptring= concat(@searchDuptring,'  and legalName ', ' like ','''%', iNlegalName,'%''');
end if;

 if(iNbusinessName!='') then
 set @searchstring=  concat(@searchstring,'  and', ' businessName ', ' like ','''%', iNbusinessName,'%'''); 
set @searchTempstring=  concat(@searchTempstring,'   and', ' businessName ', ' like ','''%', iNbusinessName,'%'''); 
set @searchDuptring= concat(@searchDuptring,'  and businessName ', ' like ','''%', iNbusinessName,'%''');
 end if;
 
 if(iNrnc!='') then
		 if(iNSearchType ='MP') then
		 begin
			set @searchstring=  concat(@searchstring,'  and', ' PassportNbr ', ' like ','''%', iNrnc,'%''');  
		 end;
		 else
		 begin
			 set @searchstring=  concat(@searchstring,'  and', ' rncnumber ', ' like ','''%', iNrnc,'%''');  
			set @searchTempstring=  concat(@searchTempstring,'  and', ' rncnumber ', ' like ','''%', iNrnc,'%''');  
            set @searchDuptring= concat(@searchDuptring,'  and rncnumber ', ' like ','''%', iNrnc,'%''');
		end;
        end if;
  end if;

if(iNprocessornbr!='') then
 set @searchstring=  concat(@searchstring,'  and', ' ProcessorNumber ', ' like ','''%', iNprocessornbr,'%'''); 
set @searchTempstring=  concat(@searchTempstring,'   and', ' ProcessorNumber ', ' like ','''%', iNprocessornbr,'%'''); 
 end if;
 
 if(iNprocessorName!='') then
 set @searchstring=  concat(@searchstring,'  and', ' ProcessorName ', ' like ','''%', iNprocessorName,'%'''); 
set @searchTempstring=  concat(@searchTempstring,'   and', ' ProcessorName ', ' like ','''%', iNprocessorName,'%'''); 
 end if;

 if(iNownerName!='') then
 set @searchstring=  concat(@searchstring,'  and', ' ownerName ', ' like ','''%', iNownerName,'%'''); 
set @searchTempstring=  concat(@searchTempstring,'   and', ' ownerName ', ' like ','''%', iNownerName,'%'''); 
set @searchDuptring= concat(@searchDuptring,'  and ownerName ', ' like ','''%', iNownerName,'%''');
 end if;

if(iNSearchType='PT') then

set @conditions=concat(@searchstring,'  union   ',@searchTempstring, ' ', 'order by taskStatusId '); 

elseif(iNSearchType='T') then
set @conditions=Concat(@searchTempstring,' ', ' and insertUserId =11000 ', 'order by taskStatusId ');
elseif(iNSearchType='D') then
set @conditions=concat(@searchDuptring,' ', 'order by merchantid');
else
set @conditions=concat(@searchstring,' ', 'order by taskStatusId ');
end if;

 PREPARE stmt FROM @conditions;
 EXECUTE stmt;
 DEALLOCATE PREPARE stmt;

end





