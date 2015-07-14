call avz_spreterieveAdminExp(89);

drop procedure if exists avz_spreterieveAdminExp;

Delimiter $$

Create Procedure avz_spreterieveAdminExp
(
iNContractId bigint
)
begin

select  AdministrativeExpenses as AdminExp from tb_contracts where contractId=iNContractId;
end;