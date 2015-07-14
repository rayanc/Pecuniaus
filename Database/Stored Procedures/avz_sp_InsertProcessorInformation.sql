
#call avz_sp_InsertProcessorInformation(238,100011);

drop procedure if exists avz_sp_InsertProcessorInformation;
delimiter $$

create procedure avz_sp_InsertProcessorInformation (merchantId bigint,merchantidTmp bigint)

begin
declare _CNetProcessorNbr,_VNetProcessorNbr,_OtherProcessorNbr,_creditDate varchar(250);

SELECT CNetProcessorNbr,VNetProcessorNbr,OtherProcessorNbr,firstcreditdate into _CNetProcessorNbr,_VNetProcessorNbr,
_OtherProcessorNbr,_creditDate FROM tmp_tb_merchants WHERE merchantid_tmp = merchantIdTmp;
 
 if(_CNetProcessorNbr<>'') then
 insert into tb_processor(processortypeid,merchantId,processorNumber) values (10001,merchantId,_CNetProcessorNbr);
 end if;
if(_VNetProcessorNbr<>'') then
 insert into tb_processor(processortypeid,merchantId,processorNumber) values (10002,merchantId,_VNetProcessorNbr);
 end if;
 if(_OtherProcessorNbr<>'') then
 insert into tb_processor(processortypeid,merchantId,processorNumber) values (10003,merchantId,_OtherProcessorNbr);
 end if;

end