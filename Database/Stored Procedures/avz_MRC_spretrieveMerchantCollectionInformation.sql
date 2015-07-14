
drop procedure if exists avz_MRC_spretrieveMerchantCollectionInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantCollectionInformation(
iNmerchantId bigint,
iNStartDate datetime,
iNEndDate datetime
)
begin

Select 
COL.CollectionId,CON.ContractID ContractId,CONST.StatusName ContractStatus,TYP.Description CollectionType,StartDate DateEntered,ClosedDate DateRemoved,CON.LoanedAmount AECAmount,
AmountinCollection AmountWhenRemovedFromCollection,1001 AffiliationId,OwnedAmount,CON.Retention RetentionTime,FundingDate,MRCST.StatusName MerchantStatus
from tb_collections COL
inner join tb_contracts CON on COL.ContractID=CON.ContractId
inner join tb_merchants MRC on MRC.MerchantId=COL.MerchantID
inner join lkp_tb_statuses CONST on CONST.statusId=CON.StatusId
inner join lkp_tb_statuses MRCST on MRCST.statusId=MRC.StatusId
inner join lkp_tb_types TYP on TYP.TypeId=COL.CollectionTypeId
Where MRC.MerchantId=iNmerchantId and
TYP.Usage='Col' and
COL.InsertDate>=IF(iNStartDate='1900-01-01',COL.InsertDate,iNStartDate) and 
COL.InsertDate<=If(iNEndDate='1900-01-01',COL.InsertDate,iNEndDate);

end;