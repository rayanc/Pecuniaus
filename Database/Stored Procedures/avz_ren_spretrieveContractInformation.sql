
drop procedure if exists avz_ren_spretrieveContractInformation;

delimiter $$
#call avz_ren_spretrieveContractInformation(67)
create procedure avz_ren_spretrieveContractInformation(iNContractId bigint)
begin

Select 
MRC.MerchantId,
MRCST.StatusName as MerchantStatus,
CNTST.StatusName as ContractStatus,
MRC.LegalName merchantName,
MRC.BusinessName ,  
1 AS everinCollection,
#tt.TaskName taskName,
'Merchant List' taskName,
#ts.StatusName taskStatus,
'open' taskStatus,
ifnull(CNT.loanedamount,0.00) as loanAmount,
ifnull(CNT.OwnedAmount,0.00) as ownedAmount,
ifnull(CNT.paidAmount,0.00) as paidAmount,
ifnull(cast(cnt.fundingDate as date ),'') as fundedDate,	
Format((ifnull(cnt.paidAmount,0.00) * 100)/ifnull(cnt.ownedAmount,0.00),2)  percentpaid,
ifnull(colcnt.expectedTurn,0.00) expectedTurn,
ifnull(colcnt.realturn,0.00) actualTurn,
CNT.contractid as contractId,
(ifnull(CNT.OwnedAmount,0.00)- ifnull(CNT.paidAmount,0.00)) as pendingAmount


From tb_contracts as CNT 
Inner join tb_merchants as MRC on CNT.MerchantId=MRC.MerchantId
inner Join lkp_tb_statuses as MRCST on MRCST.StatusId=MRC.StatusId
Left Join lkp_tb_statuses as CNTST on CNTST.StatusId=CNT.StatusId
Left join
	(Select Max(TaskTypeId)as TaskTypeId,StatusId, contractId from 	tb_tasks
	where contractId = iNContractId group by contractId		
	)tsk  ON CNT.contractId=tsk.contractId	
Left join tb_tasktypes tt on tsk.TaskTypeId = tt.TaskTypeId
#Left Join tb_tasks t on t.contractId=iNContractId and t.TaskTypeId=tsk.TaskTypeId
Left join lkp_tb_statuses ts on ts.StatusId = tsk.StatusId
left JOIN tb_collectioncontracts colcnt ON colcnt.contractid = CNT.contractid   
Where CNT.ContractId = iNContractId ;
end;