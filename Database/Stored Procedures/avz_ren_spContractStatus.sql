drop procedure if exists avz_ren_spCompleteRenewalsTask;

delimiter $$

create procedure avz_ren_spUpdateContractStatus(iContractId bigint,iStatus text )
begin

INSERT INTO `avz`.`tb_renewals`
(
`ContractID`,
`MerchantID`,
`RenewedContractID`,
`SalesRepID`,
`InsertUserID`,
`InsertDate`)
VALUES (iNcontractId,1,1,1,1,now());


end;
