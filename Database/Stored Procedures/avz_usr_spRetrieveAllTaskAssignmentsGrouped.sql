

drop procedure if exists avz_usr_spRetrieveAllTaskAssignmentsGrouped;

delimiter $$

create procedure avz_usr_spRetrieveAllTaskAssignmentsGrouped
(

)
begin
Select * from (
Select distinct
Id UserID,UserName,
SUM(IF(X.TaskName='PQ-Merchant Review',TaskCount,0)) As 'PQMerchantReview',
SUM(IF(X.TaskName='PQ-Scan Document',TaskCount,0)) As 'PQScanDocument',
SUM(IF(X.TaskName='PQ-Data Entry',TaskCount,0)) As 'PQDataEntry',
SUM(IF(X.TaskName='PQ-Monthly Credit Card Volumes',TaskCount,0)) As 'PQMonthlyCreditCardVolumes',
SUM(IF(X.TaskName='PQ-Merchant Evaluation',TaskCount,0)) As 'PQMerchantEvaluation',
SUM(IF(X.TaskName='PQ-Offer Creation',TaskCount,0)) As 'PQOfferCreation',
SUM(IF(X.TaskName='PQ-Offer Acceptance',TaskCount,0)) As 'PQOfferAcceptance',
SUM(IF(X.TaskName='CW  - Scan Document',TaskCount,0)) As 'CWScanDocument',
SUM(IF(X.TaskName='CW -Verification Call',TaskCount,0)) As 'CWVerificationCall',
SUM(IF(X.TaskName='CW -  Data Entry',TaskCount,0)) As 'CWDataEntry',
SUM(IF(X.TaskName='CW - Verification Task',TaskCount,0)) As 'CWVerificationTask',
SUM(IF(X.TaskName='CW - Review',TaskCount,0)) As 'CWReview',
SUM(IF(X.TaskName='CW - Contract',TaskCount,0)) As 'CWContract',
SUM(IF(X.TaskName='CW - Funding',TaskCount,0)) As 'CWFunding',
SUM(IF(X.TaskName='CW - Final Validation',TaskCount,0)) As 'CWFinalValidation',
SUM(IF(X.TaskName='RW - List of merchants',TaskCount,0)) As 'RWListofmerchants',
SUM(IF(X.TaskName='RW - Merchant Evaluation',TaskCount,0)) As 'RWMerchantEvaluation',
SUM(IF(X.TaskName='RW - Document Verification',TaskCount,0)) As 'RWDocumentVerification',
SUM(IF(X.TaskName='RW - Offer Creation',TaskCount,0)) As 'RWOfferCreation',
SUM(IF(X.TaskName='RW - Renewal Review',TaskCount,0)) As 'RWRenewalReview',
SUM(IF(X.TaskName='RW - Contract',TaskCount,0)) As 'RWContract',
SUM(IF(X.TaskName='RW - Funding',TaskCount,0)) As 'RWFunding',
SUM(IF(X.TaskName='RW - Final Validation',TaskCount,0)) As 'RWFinalValidation'
 From(
Select USR.Id,TTY.TaskTypeId,USR.UserName,TTY.TaskName,1 TaskCount from tb_tasks TT
Inner Join tb_tasktypes TTY on TTY.TaskTypeId=TT.TaskTypeId
Inner Join tb_users USR on USR.Id=TT.AssignedUserId) X
GROUP BY Id WITH ROLLUP) ACT;
end;
