delimiter $$

create procedure avz_gen_spRetrieveDeclineReasons()
begin 

select declinereasonid as keyId,
reasondescription as description 
from lkp_tb_declinereasons;

end $$

delimiter $$ 
