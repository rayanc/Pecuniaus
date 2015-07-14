drop procedure if exists avz_ren_spCompleteRenewalsTask;

delimiter $$

create procedure avz_ren_spCompleteRenewalsTask(iNcontractId bigint)
begin

INSERT INTO `avz`.`tb_renewals`
(
`ContractID`,
`MerchantID`,
`RenewedContractID`,
`SalesRepID`,
`InsertUserID`,
`InsertDate`)
VALUES (1,1,1,1,1,now());



end;
