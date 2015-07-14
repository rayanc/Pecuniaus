drop procedure if exists avz_MRC_spretrieveMerchantBusinessProcessorInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantBusinessProcessorInformation(iNmerchantId bigint)
begin

Select 
MRC.terminals as Terminal,PCR.ProcessorId as processorId,PTYP.Name as ProcessorName,processorNumber,TelePhone1 as ProcessorPhoneNumber,
MRC.industryTypeId as IndustryTypeID,INTYP.Name IndustryType,firstProcessedDate as FirstProcessedDate,'2014/01/01' DateGracePeriod
from tb_merchants as MRC
inner Join tb_processor PCR on PCR.MerchantID=MRC.MerchantId
inner join lkp_tb_processorlist PTYP on PTYP.ProcessorId=PCR.ProcessorTypeId
inner join lkp_tb_industrytypes INTYP on MRC.IndustryTypeID=INTYP.IndustryTypeID
Where MRC.merchantId=iNmerchantId;
end;