drop procedure if exists avz_gen_spRetrieveNoteTypes;
delimiter $$
create procedure avz_gen_spRetrieveNoteTypes()
begin
select notetypeid as keyId, description  from lkp_tb_notetypes;
end;