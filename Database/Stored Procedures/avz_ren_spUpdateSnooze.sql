drop procedure if exists avz_ren_spUpdateSnooze;

delimiter $$

create procedure avz_ren_spUpdateSnooze
(
iNContractID int,
iNSnooze datetime,
iNPercentPaid bigint
)
begin

	DECLARE CheckExists int;  
    SET CheckExists = 0;  
 
    SELECT count(*) INTO CheckExists from tb_snooze where contractID = iNContractID;

If (CheckExists>0) then
	
	update `tb_snooze` set snoozetilldate  = iNSnooze , percentpaid = iNPercentPaid 
	where  contractID = iNContractID;
Else
	INSERT INTO `tb_snooze`
		(
		`contractID`,
		`snoozetilldate`,
		`percentpaid`,
		`insertUserId`,
		`insertdate`)
		VALUES (iNContractID,iNSnooze,iNPercentPaid,1,now());
end if;

end $$
delimiter $$ 