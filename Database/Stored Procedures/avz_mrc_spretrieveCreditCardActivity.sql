drop procedure if exists avz_mrc_spretrieveCreditCardActivity;
-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: comments before and after the routine body will not be stored by the server
-- --------------------------------------------------------------------------------
DELIMITER $$

CREATE PROCEDURE `avz_mrc_spretrieveCreditCardActivity`( iNmerchantId bigint)
begin
select cc.merchantId,
cc.processorId,
cc.totalAmount,
pro.ProcessorTypeId as processorTypeId,
cc.totalTickets,
cc.Id as creditcardActivityId,
cc.month,
cc.year,
prol.name as processorName,
pro.processorNumber as processorRNC

from tb_CreditCardVolumes cc
join tb_processor pro on
cc.ProcessorId=pro.ProcessorId
join lkp_tb_processorlist prol
on pro.ProcessorTypeId= prol.ProcessorId

where cc.merchantid=iNmerchantid ;


end