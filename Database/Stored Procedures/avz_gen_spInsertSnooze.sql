drop procedure if exists avz_gen_spInsertSnooze;
delimiter $$
create procedure avz_gen_spInsertSnooze(iNcontractId bigint,iNpercentPaid double,iNsnoozeDate Datetime)
begin 

if exists (select contractid from tb_snooze where contractId =iNcontractId) then
update tb_snooze set  snoozetilldate=iNsnoozeDate, percentPaid=iNpercentPaid,modifyuserid=1,modifydate=now() where contractId=iNcontractId;

else
insert into tb_snooze(snoozetilldate,percentPaid,contractId,insertuserid,insertdate) values(iNsnoozeDate,iNpercentPaid,iNcontractId,1,now());
end if;

end;