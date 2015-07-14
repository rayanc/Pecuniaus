drop procedure if exists avz_gen_spRetrieveWorkflowTypes;

delimiter $$
Create procedure avz_gen_spRetrieveWorkflowTypes()
begin
select typ.WorkFlowId as keyId, typ.WorkFlowName as description
from tb_workflows typ Order By typ.WorkFlowName asc;

end;