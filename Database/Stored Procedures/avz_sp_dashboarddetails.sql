drop procedure if exists avz_sp_dashboarddetails;

delimiter $$

CREATE PROCEDURE `avz_sp_dashboarddetails`(
iNlogedinUser bigint
)
begin

/* Assigned Tasks  */
select wkf.WorkFlowName, typ.TaskName, mrc.LegalName, tsk.AssignedDate, tsks.StatusName, tsk.TaskTypeId
, mrc.MerchantId from tb_tasks tsk
left join lkp_tb_statuses tsks on tsks.StatusId=tsk.StatusId
left join tb_workflows wkf on wkf.WorkflowId=tsk.WorkflowId
left join tb_tasktypes typ on typ.tasktypeId=tsk.TaskTypeId
left join tb_merchants mrc on mrc.MerchantId=tsk.MerchantId
left join tmp_tb_merchants  tmrc on tmrc.MerchantId=tsk.MerchantIDTMP
where tsk.AssignedUserId=iNlogedinUser and tsk.StatusId=22002 ;

/*Collection Activities  */
select mrc.LegalName, sta.StatusName, col.AssignedDate from tb_collections col
inner join tb_merchants mrc on mrc.MerchantId=col.MerchantId
inner join lkp_tb_statuses sta on sta.StatusId=mrc.StatusId
/*where col.assigneduserId=iNlogedinUser*/;

/* Total Sales  */
select typ.Description, ifnull(cnt.fundingDate,0) as fundingDate, 
ifnull(cnt.LoanedAmount,0) as LoanedAmount, ifnull(cnt.PaidAmount,0) as PaidAmount, 
ifnull((cnt.LoanedAmount - cnt.PaidAmount),0) as BalanceAmount   
from tb_contracts cnt
inner join lkp_tb_types typ on typ.TypeId=cnt.ContractTypeId
where cnt.StatusId=20007;
/*union
select 'Renewals' as Description, '2014-10-05 00:00:00' as fundingDate,
30000 as LoanedAmount, 0 as PaidAmount, 0 as BalanceAmount;*/

/* New Leads */
select  distinct case tmrc.insertUserId when 10001 then 'Sales Force' else 'System' end as LeadSource,
mrc.LegalName from tb_tasks tsk
left join tb_workflows wkf on wkf.WorkflowId=tsk.WorkflowId
left join tb_tasktypes typ on typ.tasktypeId=tsk.TaskTypeId
left join tb_merchants mrc on mrc.MerchantId=tsk.MerchantId
left join tmp_tb_merchants  tmrc on tmrc.MerchantId=tsk.MerchantIDTMP
where typ.TaskTypeId=1 and mrc.StatusId=22002;


end