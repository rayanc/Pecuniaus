drop procedure if exists avz_sp_UpdateCallVeriAns;
delimiter $$
create procedure avz_sp_UpdateCallVeriAns(
iNmerchantId bigint,
iNContractId bigint,
iNAnswersXml text,
iNEntity varchar(20)
)
begin
	declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned;     

	CREATE TEMPORARY TABLE Answers (
		QId bigint,
		QuesText varchar(500),
		AnsText varchar(5000)
    );
     
    -- calculate the number of row elements.   
	set pRowCount  = extractValue(iNAnswersXml, 'count(//answers)');
    
	while pRowIndex <= pRowCount do
		insert into  Answers(QuesText, AnsText )
		select 
            coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@QuestText'), ''),0)
           , coalesce(nullif(extractvalue(iNAnswersXml, '//child::*[$pRowIndex]/@AnswerText'), ''),0);
		
		set pRowIndex = pRowIndex + 1;
	end while;


/* Update question ID */
	SET SQL_SAFE_UPDATES=0;
	update Answers a join tb_verificationquestions q on a.QuesText = q.Description and q.Entity=iNEntity
	set a.QId= q.QuestionId;

	update tb_verificationanswers a join Answers b on  a.QuestionId = b.QId
	set a.AnswerText=b.AnsText
	Where a.merchantId=iNMerchantId and a.contractId=iNContractId;
	
	insert into tb_verificationanswers (QuestionId, AnswerText, MerchantId, ContractId)
	select QId, AnsText, iNMerchantId, iNContractId from Answers
		Where QId not in (select QuestionId from tb_verificationanswers where merchantId=iNMerchantId and contractId=iNContractId and QuestionId is not null) ;

	Drop Table Answers;

	
end;
$$
