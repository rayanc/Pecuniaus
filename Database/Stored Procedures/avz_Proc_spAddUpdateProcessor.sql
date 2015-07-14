Use Pecuniaus;
drop procedure if exists avz_Proc_spAddUpdateProcessor;

delimiter $$
 
create procedure avz_Proc_spAddUpdateProcessor
(
  `iNProcessorId` bigint(20) ,  
   `iNName` varchar(50),
   iNprocessorCode varchar(50),
   `iNdescription` varchar(100) ,
  `iNIsActive` bool 
)

begin
if(iNProcessorId=0) then
Begin
INSERT INTO `lkp_tb_processorlist`
(
`Name`,
ProcessorCode,
Description,
IsActive
)
VALUES
(
iNName,
iNprocessorCode,
iNdescription,
iNIsActive
);
end;
else 
begin
UPDATE `lkp_tb_processorlist`
SET
`Name` = iNName,
`IsActive` =iNIsActive,
`processorCode`=iNprocessorCode,
`description`=iNdescription
WHERE `ProcessorId` = iNProcessorId;
end;
end if;
end ;


