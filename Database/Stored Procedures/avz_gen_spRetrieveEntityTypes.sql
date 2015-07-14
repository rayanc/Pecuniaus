delimiter $$

create procedure avz_gen_spRetrieveEntityTypes()
begin 

SELECT EntitytypeID as keyId, Name as description FROM lkp_tb_entitytypes;

end $$

delimiter $$ 
