-- ================================================
-- Name: AVZ_FIN_spRetriveActivities
-- 
-- Description : To Retrieve activity in finance screen
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 15-10-2014
-- ================================================
drop procedure if exists AVZ_FIN_spRetriveActivities;

DELIMITER //
CREATE PROCEDURE AVZ_FIN_spRetriveActivities 
(iNMerchantId bigint,
iNActivityTypeId int,
iNDateOfActivity date,
iNAffiliation int, 
iNProcessorId int)
BEGIN

declare searchstring longtext;


set @searchstring='Select pra.ActivityId,pra.ContractID,
    pra.processedDate as dateOfActivity, ltp.Name as processorName, off.retention, ifnull(pra.AppliedAmount,0) totalAmount,
	if (pra.ActivityTypeId in (1,2), pra.AppliedAmount, 0) as  incomeThroughProcessor,
	if (pra.ActivityTypeId not in (1,2), pra.AppliedAmount,0) as  otherIncome,
	ifnull((((ct.OwnedAmount-ct.LoanedAmount)/ct.OwnedAmount))*pra.AppliedAmount,0) as price,
	ifnull((1-((ct.OwnedAmount-ct.LoanedAmount)/ct.OwnedAmount))*pra.AppliedAmount,0) as capital,
	act.ActivityName as activityType, ct.contractNumber, pra.notes
from
    tb_processoractivities pra
	left join lkp_tb_processorlist ltp on pra.ProcessorId=ltp.ProcessorId
	left join tb_offers off on pra.ContractID=off.ContractID
	left join lkp_tb_financeactivitylist act on pra.ActivityTypeId=act.ActivityId
	left join tb_merchantaffiliations maf on pra.MerchantId=maf.MerchantId
	left join tb_contracts ct on pra.ContractId=ct.ContractId where ltp.IsActive=1';

if(iNMerchantId!=0) then 
set @searchstring=  concat(@searchstring,'  and pra.MerchantId =',iNMerchantId);
end if;

if(iNActivityTypeId!=0) then 
set @searchstring=  concat(@searchstring,'  and pra.ActivityTypeId =',iNActivityTypeId);
end if;

if(iNDateOfActivity!='0000-00-00') then 
set @searchstring=  concat(@searchstring,'  and pra.ProcessedDate =','''',iNDateOfActivity,'''');
end if;

if(iNAffiliation!=0) then 
set @searchstring=  concat(@searchstring,'  and maf.AffiliationNumber =',iNAffiliation);
end if;

if(iNProcessorId!=0) then 
set @searchstring=  concat(@searchstring,'  and pra.ProcessorId =',iNProcessorId);
end if;
PREPARE stmt FROM @searchstring;	 

EXECUTE stmt;
DEALLOCATE PREPARE stmt;

END;
//
DELIMITER ;
