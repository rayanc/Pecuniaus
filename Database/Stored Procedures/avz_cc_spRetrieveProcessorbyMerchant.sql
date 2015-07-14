drop procedure if exists avz_cc_spRetrieveProcessorbyMerchant;

delimiter $$

create procedure avz_cc_spRetrieveProcessorbyMerchant(iNmerchantId bigint)
begin

Select 
PCR.ProcessorId as processorId,Name as processorName,processorNumber as processorRNC, PCR.ProcessorTypeId
from tb_merchants as MRC
inner Join tb_processor PCR on PCR.MerchantID=MRC.MerchantId
inner join lkp_tb_processorlist PTYP on PTYP.ProcessorId=PCR.ProcessorTypeId
Where MRC.merchantId=iNmerchantId;
end;