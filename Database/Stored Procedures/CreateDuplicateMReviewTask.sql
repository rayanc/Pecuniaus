drop procedure if exists CreateDuplicateMReviewTask;
delimiter $$
create procedure CreateDuplicateMReviewTask
(
iNTaskTypeId bigint,
iNStatusId bigint,
iNpriority bigint,
iNstartDate datetime,
iNmerchantId bigint,
iNcontractId bigint,
iNaffiliationFlag smallint,
iNmerchantIdTmp bigint,
iNworkflowId bigint,
iNInsertUserId bigint

)
begin



update tb_tasks set StatusID=22005 where MerchantId=iNmerchantIdTmp and StatusID in (22002,22001);

insert into tb_tasks(TaskTypeId,StatusId,priority,startDate,contractId,affiliationFlag,merchantId
,workflowId, assigneduserId, AssignedDate ) 
values(iNTaskTypeId,iNStatusId,iNpriority,iNstartDate,iNcontractId,iNaffiliationFlag,iNmerchantIdTmp
,iNworkflowId,iNInsertUserId, NOW());

update tb_contracts set StatusId= 21002 where ContractID= AVZ_GEN_FNRETRIEVEACTIVECONTRACT(iNmerchantId);
update tb_merchants set StatusId=20001 where merchantId=iNmerchantId;


end