drop procedure if exists avz_cc_spSaveEvaluationTask;
delimiter $$

create procedure avz_cc_spSaveEvaluationTask
(iNtasktypeid bigint, 
iNmerchantId bigint, 
iNcontractId bigint,
iNscore double,
iNroundedscore double,
iNfinalletter varchar(2),
iNiscompleted int,
iNworkflowid bigint,
iNrawdata text,iNrequestxml text)

begin 


insert into tb_scorecard(
merchantId,contractid,score,scoreletter,dateevaluated,scorerodendo,rawdata,requestxml
) 
values(
iNmerchantId,iNcontractId,iNscore,iNfinalletter,now(),iNroundedscore,iNrawdata,iNrequestxml
);

if(iNiscompleted=1) then 
begin
call avz_tsk_spCompleteTask(iNmerchantId,iNtasktypeid,iNworkflowid,iNcontractId);
end;
end if;
end;
