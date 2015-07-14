
drop procedure if exists avz_ren_spretrieveContractInformation;

delimiter $$

create procedure avz_ren_spretrieveContractInformation(iNContractId bigint)
begin

Select 
MRC.MerchantId,
MRCST.StatusName as MerchantStatus,
CNTST.StatusName as ContractStatus,
MRC.LegalName,
MRC.BusinessName,  
1 AS everinCollection,
tt.TaskName taskName,
ts.StatusName taskStatus,
ifnull(CNT.loanedamount,0.00) as loanAmount,
ifnull(CNT.OwnedAmount,0.00) as ownedAmount,
ifnull(CNT.paidAmount,0.00) as paidAmount,
ifnull(CNT.fundingDate ,'1900-01-01') as fundedDate,
0.0 AS percentPaid,
ifnull(colcnt.expectedTurn,0.00) expectedTurn,
ifnull(colcnt.realturn,0.00) actualTurn,
CNT.contractid as contractId


From tb_contracts as CNT 
Inner join tb_merchants as MRC on CNT.MerchantId=MRC.MerchantId
inner Join lkp_tb_statuses as MRCST on MRCST.StatusId=MRC.StatusId
inner Join lkp_tb_statuses as CNTST on CNTST.StatusId=CNT.StatusId
Left Join
	(Select Max(TaskTypeId)as TaskTypeId,StatusId,contractId from 	tb_tasks
	where StatusId=22002 and contractId = iNContractId group by StatusId,contractId  
	)tsk  ON CNT.contractId=tsk.contractId	
Left join tb_tasktypes tt on tsk.TaskTypeId = tt.TaskTypeId
Left join lkp_tb_statuses ts on tsk.StatusId = ts.StatusId
left JOIN tb_collectioncontracts colcnt ON colcnt.contractid = CNT.contractid   
Where CNT.ContractId = iNContractId ;
end;