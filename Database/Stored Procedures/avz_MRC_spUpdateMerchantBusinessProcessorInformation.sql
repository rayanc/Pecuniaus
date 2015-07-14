drop procedure if exists avz_MRC_spUpdateMerchantBusinessProcessorInformation;

delimiter $$

create procedure avz_MRC_spUpdateMerchantBusinessProcessorInformation
(
iNterminal int,iNprocessorTypeId int,iNprocessorNumber varchar(200),iNdateGracePeriod date,
iNindustryTypeID int,iNfirstProcessedDate date,iNprocessorId int,iNmerchantId bigint
)
begin
start transaction;
Update tb_processor set ProcessorTypeId=iNprocessorTypeId, processorNumber=iNprocessorNumber,
firstProcessedDate=iNfirstProcessedDate
where ProcessorId=iNprocessorId;

Update tb_merchants set terminals=iNterminal,IndustryTypeID=iNindustryTypeID where merchantId=iNmerchantId;
Commit;
end;