drop procedure  if exists avz_ren_spRetrievRenewalsList;
##call avz_ren_spRetrievRenewalsList('')
delimiter $$ 
create procedure avz_ren_spRetrievRenewalsList
(
iNFilter varchar(200)
)
begin
SELECT distinct
	mrc.legalName,
	mrc.merchantId,
	ifnull(mrc.statusId,0) as statusId,	
	ifnull(mts.StatusName,'') as statusname,
	#tt.TaskName taskName,
	'Merchant List' taskName,
	#ifnull(ts.StatusName,'')as taskStatus,
	'open' taskStatus,
	ifnull(cnt.statusid,'')as statusid,    
	ifnull(cnt.LoanedAmount,0.00) as loanAmount,
	ifnull(cnt.ownedAmount,0.00) as ownedAmount,
	ifnull(cnt.paidAmount,0.00) as paidAmount,
	ifnull(cast(cnt.fundingDate as date ),'') as fundedDate,	
	ifnull(cnt.Turn,0) expectedTurn,
	ifnull(cnt.realturn,0) actualTurn,
	Format((ifnull(cnt.paidAmount,0.00) * 100)/ifnull(cnt.ownedAmount,0.00),2)  percentpaid,
	0 everInCollection,
	cnt.contractid as contractId
FROM
    tb_renewalslist ren
	JOIN
    tb_contracts cnt ON ren.contractid = cnt.contractid
	JOIN
    tb_merchants mrc ON mrc.merchantid = cnt.merchantid
	Left JOIN 
	(Select max(TaskTypeId)TaskTypeId,StatusId,contractId from tb_tasks		
		group by contractId  	
	)tsk  ON ren.contractId=tsk.contractId	
	Left JOIN 
	tb_tasktypes tt on tsk.TaskTypeId = tt.TaskTypeId
	Left join 
	lkp_tb_statuses ts on tsk.StatusId = ts.StatusId
	Left join 
	lkp_tb_statuses mts on mrc.StatusId = mts.StatusId
	left JOIN
    tb_collectioncontracts colcnt ON colcnt.contractid = cnt.contractid    
	Left JOIN 
	tb_snooze snz on snz.Contractid=cnt.contractid
	Left JOin 
	tb_renewals renw on cnt.ContractId = renw.ContractID
	Left join tb_declinereasons dc on ren.ContractID = dc.contractId and dc.workflowid=3

	
	where
	now()<= ifnull(snz.snoozetilldate,now()) AND renw.ContractID is null and dc.DeclineReasonID IS null;
	
 
end;
    