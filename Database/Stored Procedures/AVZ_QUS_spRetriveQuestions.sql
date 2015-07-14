drop procedure if exists AVZ_QUS_spRetriveQuestions;
-- ================================================
-- Name: AVZ_QUS_spRetriveQuestions
-- 
-- Description : To Retrieve Questions for particular Entity
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 09-09-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_QUS_spRetriveQuestions 
(iNEntity varchar(100),
iNContractId bigint)
BEGIN

If iNContractId <= 0 Then
Select 
    questionId, Description as questionDesc, questionType
from
    tb_verificationquestions	
where   
	Entity = iNEntity and isActive = 1;

ELSE

Select 
    vq.questionId, Description as questionDesc, questionType, answerId, AnswerText as answerDesc
from
    tb_verificationquestions vq
	left join tb_verificationanswers va on vq.QuestionId=va.QuestionId And va.ContractId = iNContractId

where    
	vq.Entity=iNEntity and isActive = 1 ;

select ScriptFile from tb_verificationScript  where EntityFor=iNEntity and ContractId = iNContractId;
END IF;


END;
//
DELIMITER ;
