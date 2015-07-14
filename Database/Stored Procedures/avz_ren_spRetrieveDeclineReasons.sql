
drop procedure if exists avz_ren_spRetrieveDeclineReasons;

delimiter $$ 
create procedure avz_ren_spRetrieveDeclineReasons()
begin
SELECT 
DeclineReasonId,
ReasonDescription

FROM `lkp_tb_declinereasons`
where IsActive=1;

end $$
delimiter $$
