-- ================================================
-- Name: AVZ_QUS_spInsUpdateAnswers
-- 
-- Description : To Insert Update answers for particular entity and contractid
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 10-09-2014
-- ================================================
drop procedure if exists AVZ_QUS_spInsUpdateAnswers;
DELIMITER //
CREATE PROCEDURE AVZ_QUS_spInsUpdateAnswers 
(iNAnswersXml text,
 iNtaskTypeId bigint,
 iNworkflowId bigint, 
 iNisCompleted smallint,
 iNentity varchar(20),
 iNscriptFile varchar(500)
)
BEGIN

    declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned;     
	declare pMerchantId bigint;
	declare pContractId bigint;	
	-- declare pScript varchar(250);	

	CREATE TEMPORARY TABLE Answers (
    AnsId bigint, 
    QuesId bigint,
	AnsText varchar(5000),
	ContId bigint,
	MrcId bigint
	-- ,ScriptFile varchar(250)
    );
     
    -- calculate the number of row elements.   
     set pRowCount  = extractValue(iNAnswersXml, 'count(//answers)');
    
	 while pRowIndex <= pRowCount do
      insert into  Answers
      select 
            coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@AnswerId'), ''),0)
           , coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@QuestionId'), ''),0)
           , coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@AnswerText'), ''),0)
		   , coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@ContractId'), ''),0)
		   , coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@MerchantId'), ''),0);
		  -- , coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@scriptFile'), ''),0);
      set pRowIndex = pRowIndex + 1;

	 end while;

	 select MrcId into pMerchantId from Answers limit 1;
	 select ContId into pContractId from Answers limit 1;
	-- select ScriptFile into pScript from Answers limit 1;


	 insert into  tb_verificationanswers(AnswerId, QuestionId, AnswerText, ContractId,MerchantId)
	 select AnsId,QuesId,AnsText,ContId,MrcId from Answers
	 on duplicate key update AnswerId = values(AnswerId), QuestionId = values(QuestionId),  AnswerText = values(AnswerText),  ContractId = values(ContractId),  MerchantId = values(MerchantId);
	 
	if (iNscriptFile !='') then
	begin
	call avz_cnt_spInsertVerificationScript(pContractId,iNscriptFile ,iNentity);
	end;
	end if;

	 Drop Table Answers;
	 
	if iNisCompleted = 1 then	
	call avz_tsk_spCompleteTask(pMerchantId,iNtaskTypeId,iNworkflowId,pContractId);
    end if;
	 
END;
//
DELIMITER ;
