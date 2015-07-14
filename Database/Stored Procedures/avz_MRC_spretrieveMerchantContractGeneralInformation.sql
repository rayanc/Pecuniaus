
drop procedure if exists avz_MRC_spretrieveMerchantContractGeneralInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantContractGeneralInformation(
iNcontractId bigint
)
begin

Select 
contractId as ContractId,ContractNumber,LoanedAmount,OwnedAmount,StatusName ContractStatus,fundingDate FundingDate,
Retention RetentionPercentage,10 AdministrativeExpenses,RetentionChangeReason,
If((PaidAmount/OwnedAmount)/100 Is Null,0,(PaidAmount/OwnedAmount)/100) PaidPercentage
from  tb_contracts CNT
inner join lkp_tb_statuses ST on CNT.StatusId=ST.StatusId
Where CNT.contractId=iNcontractId; 

end;