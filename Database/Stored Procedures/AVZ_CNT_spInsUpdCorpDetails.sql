-- ================================================
-- Name: AVZ_CNT_spInsUpdCorpDetails
-- 
-- Description : To Insert or Update corporate details of a contract
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 11-09-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_CNT_spInsUpdCorpDetails 
(iNCorpXml text)
BEGIN

    declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned;     
	
	CREATE TEMPORARY TABLE CorpDetails (
    nameOfCompany varchar(200), 
    addressDesc varchar(200),
	OwnerName varchar(200),
	OwnerLastName varchar(200)	
    );
     
    -- calculate the number of row elements.   
     set pRowCount  = extractValue(iNCorpXml, 'count(//corpDetails)');
    
    while pRowIndex <= pRowCount do
      insert into  Answers
      select 
            coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@AnswerId'), ''),0)
           , coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@QuestionId'), ''),0)
           , coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@AnswerText'), ''),0)
		   , coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@ContractId'), ''),0);
   
      set pRowIndex = pRowIndex + 1;

	 end while;
	 
	 insert into  tb_verificationanswers(AnswerId, QuestionId, AnswerText, ContractId)
	 select AnsId,QuesId,AnsText,ContId from Answers
	 on duplicate key update AnswerId = values(AnswerId), QuestionId = values(QuestionId),  AnswerText = values(AnswerText),  ContractId = values(ContractId);
	 
	 Drop Table Answers;
	 
END;
//
DELIMITER ;
