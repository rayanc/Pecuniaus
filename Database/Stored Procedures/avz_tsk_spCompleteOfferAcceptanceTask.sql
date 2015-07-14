-- ================================================
-- Name: avz_tsk_spCompleteTask
-- 
-- Description : To complete the tasks for Merchants and Contract
-- 
-- Parameters : None 
-- 
-- Author  : Tarun Bansal
-- 
-- Creation Date : 25-09-2014
--
-- Modified Date: 07-10-2014
-- Modified By: Sachin Verma
-- Added Contract Id 
-- ================================================
drop procedure if exists avz_tsk_spCompleteOfferAcceptanceTask;
DELIMITER //
create procedure avz_tsk_spCompleteOfferAcceptanceTask
(
iNomerchantId bigint,
iNotasktypeId bigint,
iNoworkflowId bigint,
iNocontractId bigint
)
begin
declare nextTask bigint;
declare _sequence bigint;
start transaction;
select 
    tasktypeId
into nextTask from
    tb_tasktypes
where
    workflowid = 2
        and sequence = (select 
            sequence
        from
            tb_tasktypes
        where
            workflowid = 2
                and tasktypeid = 8); 


If iNocontractId <= 0 Then				
update tb_tasks 
set 
    StatusId = 22004
where
    tasktypeId = iNotasktypeId
        and merchantId = iNomerchantId;
call avz_tsk_spCreateTask(nextTask,22001,1,now(),iNomerchantId,0,1,null,iNoworkflowId);
ELSE
update tb_tasks 
set 
    StatusId = 22004
where
    tasktypeId = iNotasktypeId
        and merchantId = iNomerchantId;
		/*and contractId = iNcontractId*/
call avz_tsk_spCreateTask(nextTask,22001,1,now(),iNomerchantId,iNocontractId,1,null,iNoworkflowId);	
call AVZ_TSK_spAssignTasks(); 	
END IF;

/*Insert into tb_documents(DocumentTypeId,MerchantId,ContractId,StatusId) values(1,iNomerchantId,iNocontractId,110001);
Insert into tb_documents(DocumentTypeId,MerchantId,ContractId,StatusId) values(2,iNomerchantId,iNocontractId,110001);
Insert into tb_documents(DocumentTypeId,MerchantId,ContractId,StatusId) values(3,iNomerchantId,iNocontractId,110001);
Insert into tb_documents(DocumentTypeId,MerchantId,ContractId,StatusId) values(4,iNomerchantId,iNocontractId,110001);
Insert into tb_documents(DocumentTypeId,MerchantId,ContractId,StatusId) values(5,iNomerchantId,iNocontractId,110001);
Insert into tb_documents(DocumentTypeId,MerchantId,ContractId,StatusId) values(6,iNomerchantId,iNocontractId,110001);
Insert into tb_documents(DocumentTypeId,MerchantId,ContractId,StatusId) values(7,iNomerchantId,iNocontractId,110001);
Insert into tb_documents(DocumentTypeId,MerchantId,ContractId,StatusId) values(8,iNomerchantId,iNocontractId,110001); */

update tb_contracts set statusId=20002 where contractId=iNocontractId;


commit;
end;
//
DELIMITER ;