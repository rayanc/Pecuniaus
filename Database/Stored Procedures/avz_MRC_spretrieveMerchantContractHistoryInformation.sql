
drop procedure if exists avz_MRC_spretrieveMerchantContractHistoryInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantContractHistoryInformation(
iNcontractId bigint,
iNHistoryStartDate datetime,
iNHistoryEndDate datetime
)
begin

Select 
ContractId,HistoryDate,Field,OldValue,NewValue
from tb_contracthistory HIS
Where HIS.ContractId=iNcontractId and 
HIS.HistoryDate>=IF(iNHistoryStartDate='1900-01-01',HIS.HistoryDate,iNHistoryStartDate) and 
HIS.HistoryDate<=If(iNHistoryEndDate='1900-01-01',HIS.HistoryDate,iNHistoryEndDate);

end;