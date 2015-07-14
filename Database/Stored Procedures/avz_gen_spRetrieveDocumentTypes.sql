drop procedure if exists avz_gen_spRetrieveDocumentTypes;

delimiter $$
Create procedure avz_gen_spRetrieveDocumentTypes()
begin
select typ.DocumentTypeId as keyId, typ.Description as description
from lkp_tb_documenttypes typ where WorkflowId=2 Order By typ.Description asc;

end;