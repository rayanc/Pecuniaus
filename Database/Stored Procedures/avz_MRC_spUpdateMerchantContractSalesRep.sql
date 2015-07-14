drop procedure if exists avz_MRC_spUpdateMerchantContractSalesRep;

delimiter $$

create procedure avz_MRC_spUpdateMerchantContractSalesRep
(
iNcontractId bigint,
iNSalesRepId int,
iNIsPrimarySalesRep bit,
iNUserId bigint
)
begin
start transaction;
If(iNIsPrimarySalesRep=true) then
begin
Update tb_contractsalesrep set IsPrimary=false where contractId=iNcontractId;

Update tb_contractsalesrep set IsPrimary=iNIsPrimarySalesRep where contractId=iNcontractId and salesRepId=iNSalesRepId;

Update tb_contracts set SalesRepID=iNSalesRepId where contractid=iNcontractId;
end;
else
begin
Update tb_contractsalesrep set IsPrimary=iNIsPrimarySalesRep where contractId=iNcontractId and salesRepId=iNSalesRepId;

Update tb_contracts set SalesRepID=iNSalesRepId where contractid=iNcontractId;
end;
end if;
commit;
end;