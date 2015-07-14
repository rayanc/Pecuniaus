
drop procedure if exists avz_MRC_spretrieveMerchantAffiliationInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantAffiliationInformation(
iNmerchantId bigint,
iNRequestType Varchar(10)
)
begin
if(iNRequestType='RNC') then
begin
Select 
MRC.merchantId MerchantId,MRC.LegalName MerchantName,CON.LoanedAmount AECAmount,OwnedAmount,(ownedAmount-PaidAmount) PendingAmount,
CON.Retention RetentionTime,FundingDate,MRCST.StatusName MerchantStatus
from tb_merchantaffiliations AFL
inner join tb_merchants MRC on MRC.RNCNumber=AFL.RNCNumber
inner join tb_contracts CON on MRC.MerchantId=CON.MerchantId
inner join lkp_tb_statuses MRCST on MRCST.statusId=MRC.StatusId
Where AFL.MerchantId=iNmerchantId and CON.contractId=avz_gen_fnRetrieveActiveContract(MRC.merchantId);

if ((select count(MerchantId) from tb_creditcardactivity where MerchantId=iNmerchantId)>0) then
begin
Select 
MRC.merchantId MerchantId,MRC.LegalName MerchantName,CreationDate,SC.dateevaluated LastEvaluationDate,avg(CCA.TotalAmount) RequestedCCVolumes,
'Missing' VolumeStatus
from tb_merchantaffiliations AFL
inner join tb_merchants MRC on MRC.RNCNumber=AFL.RNCNumber
inner join tb_contracts CON on MRC.MerchantId=CON.MerchantId
inner join lkp_tb_statuses MRCST on MRCST.statusId=MRC.StatusId
inner join tb_scorecard SC on SC.merchantid=MRC.MerchantId and SC.contractid=CON.ContractId
inner join tb_creditcardactivity CCA on CCA.MerchantId=MRC.MerchantId
Where AFL.MerchantId=iNmerchantId and CON.contractId<>avz_gen_fnRetrieveActiveContract(MRC.merchantId);
end;
else
begin
Select 
MRC.merchantId MerchantId,MRC.LegalName MerchantName,CreationDate,SC.dateevaluated LastEvaluationDate,0 RequestedCCVolumes,
'Missing' VolumeStatus
from tb_merchantaffiliations AFL
inner join tb_merchants MRC on MRC.RNCNumber=AFL.RNCNumber
inner join tb_contracts CON on MRC.MerchantId=CON.MerchantId
inner join lkp_tb_statuses MRCST on MRCST.statusId=MRC.StatusId
inner join tb_scorecard SC on SC.merchantid=MRC.MerchantId and SC.contractid=CON.ContractId
inner join tb_creditcardactivity CCA on CCA.MerchantId=MRC.MerchantId
Where AFL.MerchantId=iNmerchantId and CON.contractId<>avz_gen_fnRetrieveActiveContract(MRC.merchantId);
end;
end if;

end;
else
begin
Select 
MRC.merchantId MerchantId,MRC.LegalName MerchantName,CON.LoanedAmount AECAmount,OwnedAmount,(ownedAmount-PaidAmount) PendingAmount,
CON.Retention RetentionTime,FundingDate,MRCST.StatusName MerchantStatus
from tb_merchantaffiliations AFL
inner join tb_merchants MRC on MRC.OwnerId=AFL.OwnerId
inner join tb_contracts CON on MRC.MerchantId=CON.MerchantId
inner join lkp_tb_statuses MRCST on MRCST.statusId=MRC.StatusId
Where AFL.MerchantId=iNmerchantId and CON.contractId=avz_gen_fnRetrieveActiveContract(MRC.merchantId);

if ((select count(MerchantId) from tb_creditcardactivity where MerchantId=iNmerchantId)>0) then
begin
Select 
MRC.merchantId MerchantId,MRC.LegalName MerchantName,CreationDate,SC.dateevaluated LastEvaluationDate,avg(CCA.TotalAmount) RequestedCCVolumes,
'Missing' VolumeStatus
from tb_merchantaffiliations AFL
inner join tb_merchants MRC on MRC.OwnerId=AFL.OwnerId
inner join tb_contracts CON on MRC.MerchantId=CON.MerchantId
inner join lkp_tb_statuses MRCST on MRCST.statusId=MRC.StatusId
inner join tb_scorecard SC on SC.merchantid=MRC.MerchantId and SC.contractid=CON.ContractId
inner join tb_creditcardactivity CCA on CCA.MerchantId=MRC.MerchantId
Where AFL.MerchantId=iNmerchantId and CON.contractId<>avz_gen_fnRetrieveActiveContract(MRC.merchantId);
end;
else
begin
Select 
MRC.merchantId MerchantId,MRC.LegalName MerchantName,CreationDate,SC.dateevaluated LastEvaluationDate,0 RequestedCCVolumes,
'Missing' VolumeStatus
from tb_merchantaffiliations AFL
inner join tb_merchants MRC on MRC.OwnerId=AFL.OwnerId
inner join tb_contracts CON on MRC.MerchantId=CON.MerchantId
inner join lkp_tb_statuses MRCST on MRCST.statusId=MRC.StatusId
inner join tb_scorecard SC on SC.merchantid=MRC.MerchantId and SC.contractid=CON.ContractId
inner join tb_creditcardactivity CCA on CCA.MerchantId=MRC.MerchantId
Where AFL.MerchantId=iNmerchantId and CON.contractId<>avz_gen_fnRetrieveActiveContract(MRC.merchantId);
end;
end if;

end;
end if;

end;