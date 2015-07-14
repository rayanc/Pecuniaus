drop procedure if exists avz_gen_spRetrieveNoteType;
delimiter $$
Create procedure avz_gen_spRetrieveNoteType()
Begin
select NoteTypeId as keyId, description from lkp_tb_notetypes;
end;
