use Pecuniaus;
drop procedure if exists avz_proc_CheckprocessorNameExist;
DELIMITER $$
create PROCEDURE avz_proc_CheckprocessorNameExist(
iNName nvarchar(100),
iNProcessorId int
)
BEGIN
if exists(SELECT processors.`Name` FROM lkp_tb_processorlist as processors WHERE processors.`Name` = iNName) then
begin
   if(iNProcessorId>0) then
	begin
      if exists(SELECT processors.`Name` FROM lkp_tb_processorlist as processors
      WHERE processors.`Name` = iNName and processors.ProcessorId<>iNProcessorId) then
		select 1 as ID;
	  else
      select 0 as ID;
	 end if;
    end;
else
select 1 as ID;
end if;
end;
else
select 0 as ID;
end if;
end;
   
 
       
      
      
