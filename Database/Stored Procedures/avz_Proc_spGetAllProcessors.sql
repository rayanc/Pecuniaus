Use Pecuniaus;
drop procedure if exists avz_Proc_spGetAllProcessors;

delimiter $$
 
create procedure avz_Proc_spGetAllProcessors()
begin
SELECT 
ProcessorId,
`Name`,
ProcessorCode,
Description,
IsActive
 FROM Pecuniaus.lkp_tb_processorlist
 ;
 end
