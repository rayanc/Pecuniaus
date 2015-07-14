drop procedure if exists avz_mrc_spUpdateStatusContact;
delimiter $$
create procedure avz_mrc_spUpdateStatusContact(iNaccountId varchar(200), iNcontactId bigint)
begin

update tb_contacts set sfid= iNaccountId  where contactId= iNcontactId;

end;

