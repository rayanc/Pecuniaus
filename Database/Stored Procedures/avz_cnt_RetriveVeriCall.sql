drop procedure if exists avz_cnt_RetriveVeriCall;
-- ================================================
-- Name: avz_con_RetriveVeriCall
-- 
-- Description : To Retrieve Contract Verification Call Questions
-- 
-- Parameters : ContractId
-- 
-- Author  : 
-- 
-- Creation Date : 01-01-2015
-- ================================================
DELIMITER //
CREATE PROCEDURE avz_cnt_RetriveVeriCall
(
	iNContractId bigint,
	iNEntity varchar(20)
)
BEGIN

	Select 
		vq.questionId, Description as questionDesc, questionType, answerId, IFNULL(AnswerText, 'true') as answerDesc
	from
		tb_verificationquestions vq
		left join tb_verificationanswers va on vq.QuestionId=va.QuestionId And va.ContractId = iNContractId
	where    
		vq.Entity=iNEntity and isActive = 1 ;

	select ScriptFile from tb_verificationScript where EntityFor=iNEntity and ContractId = iNContractId;

END;
//
DELIMITER ;
