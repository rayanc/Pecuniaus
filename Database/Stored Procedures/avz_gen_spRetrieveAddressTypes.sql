delimiter $$
create procedure avz_gen_spRetrieveAddressTypes()
begin
SELECT AddressTypeId as keyId, AddressType as description FROM lkp_tb_addresstype;
end $$

delimiter $$