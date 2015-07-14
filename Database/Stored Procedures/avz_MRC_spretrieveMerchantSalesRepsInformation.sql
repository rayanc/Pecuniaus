
drop procedure if exists avz_MRC_spretrieveMerchantSalesRepsInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantSalesRepsInformation(iNcontractId bigint,iNsalesRepID bigint)
begin
if(iNsalesRepID=0) then
select 
SR.salesRepId SalesRepId,CNT.FirstName SalesRepName,TYP.Description SaleType,AgreementType,Commission,RenewalCommission,IsPrimary
from tb_salesrep SR
inner join tb_contractsalesrep CSR on CSR.SalesRepID=SR.SalesRepId
inner join tb_contacts CNT on CNT.contactId=SR.contactId
inner join tb_contracts CON on CSR.contractId=CON.contractId ##and CSR.ContractSalesRepId=CON.SalesRepID
inner join lkp_tb_types TYP on TYP.TypeId=CON.ContractTypeId
where CON.contractId=iNcontractId and TYP.Usage='CNT';
elseif(iNsalesRepID>0) then
select 
SR.salesRepId SalesRepId,CNT.FirstName SalesRepName,TYP.Description SaleType,AgreementType,Commission,RenewalCommission,IsPrimary
from tb_salesrep SR
inner join tb_contractsalesrep CSR on CSR.SalesRepID=SR.SalesRepId
inner join tb_contacts CNT on CNT.contactId=SR.contactId
inner join tb_contracts CON on CSR.contractId=CON.contractId ##and CSR.ContractSalesRepId=CON.SalesRepID
inner join lkp_tb_types TYP on TYP.TypeId=CON.ContractTypeId
where SR.salesRepId=iNsalesRepID and TYP.Usage='CNT';
end if;
end;