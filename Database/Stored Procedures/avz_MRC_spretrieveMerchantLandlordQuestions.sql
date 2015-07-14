
drop procedure if exists avz_MRC_spretrieveMerchantLandlordQuestions;

delimiter $$

create procedure avz_MRC_spretrieveMerchantLandlordQuestions(iNmerchantId bigint)
begin
Select
Answertext as Answer,Description as Question
from tb_verificationanswers VA
inner join tb_verificationquestions VQ on VA.QuestionId=VQ.QuestionId
Where VA.MerchantId=iNmerchantId;
end;