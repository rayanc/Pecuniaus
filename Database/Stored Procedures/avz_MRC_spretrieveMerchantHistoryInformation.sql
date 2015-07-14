
drop procedure if exists avz_MRC_spretrieveMerchantHistoryInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantHistoryInformation(
iNmerchantId bigint,
iNHistoryStartDate datetime,
iNHistoryEndDate datetime
)
begin

Select 
MerchantId,HistoryDate,Field,OldValue,NewValue
from tb_merchanthistory HIS
Where HIS.MerchantId=iNmerchantId and 
HIS.HistoryDate>=IF(iNHistoryStartDate='1900-01-01',HIS.HistoryDate,iNHistoryStartDate) and 
HIS.HistoryDate<=If(iNHistoryEndDate='1900-01-01',HIS.HistoryDate,iNHistoryEndDate);

/*from  (
Select 18 MerchantId,cast('2014-01-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
Union
Select 18 MerchantId,cast('2014-02-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
Union
Select 18 MerchantId,cast('2014-03-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
Union
Select 18 MerchantId,cast('2014-04-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
Union
Select 18 MerchantId,cast('2014-05-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
Union
Select 18 MerchantId,cast('2014-06-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
Union
Select 18 MerchantId,cast('2014-07-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
Union
Select 18 MerchantId,cast('2014-08-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
Union
Select 18 MerchantId,cast('2014-09-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
Union
Select 18 MerchantId,cast('2014-10-01' as datetime) HistoryDate, 'myField' Field,'oldValue' OldValue,'newValue' NewValue
) HIS*/

end;