drop procedure if exists avz_ren_spRetrieveSnoozeDetails;
#call avz_ren_spRetrieveSnoozeDetails(2)
delimiter $$

create procedure avz_ren_spRetrieveSnoozeDetails
(
iNContractID int
)
begin

	
	select 		
		`contractID` as contractId,
		`snoozetilldate` as snoozeDate,
		`percentpaid` as snoozePercent		
	from 
		`tb_snooze`
	where contractID=iNContractID;

end $$
delimiter $$ 