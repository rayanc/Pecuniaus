-- ================================================
-- Name: avz_tsk_spCreateTask
-- 
-- Description : To create the tasks for Merchants and Contract
-- 
-- Parameters : None 
-- 
-- Author  : Tarun Bansal
-- 
-- Creation Date : 25-09-2014

-- ================================================
drop procedure if exists avz_tsk_spCreateTask;
DELIMITER //
create procedure avz_tsk_spCreateTask
(
	iNTaskTypeId bigint,
	iNStatusId bigint,
	iNpriority bigint,
	iNstartDate datetime,
	iNmerchantId bigint,
	iNcontractId bigint,
	iNaffiliationFlag smallint,
	iNmerchantIdTmp bigint,
	iNworkflowId bigint
)
begin
	/*declare _propertyType bigint default 0;*/

	insert into tb_tasks(TaskTypeId,StatusId,priority,startDate,merchantId,contractId,affiliationFlag,merchantIdTmp,workflowId ) 
	values(iNTaskTypeId,iNStatusId,iNpriority,iNstartDate,iNmerchantId,iNcontractId,iNaffiliationFlag,iNmerchantIdTmp,iNworkflowId );

	/*select RentFlag into _propertyType from tb_merchants  where merchantid=iNmerchantId;
	
	If property type is owned then we dont need to create a landlord verification task rather we will mark that as completed
	*/
	/*if( _propertyType=200002 and iNTaskTypeId=14 ) then 
		set max_sp_recursion_depth=1;
	
		call avz_tsk_spCompleteTask(iNmerchantId, iNtasktypeId,	iNworkflowId, iNcontractId);
	end if;*/
end;
//
DELIMITER ;
