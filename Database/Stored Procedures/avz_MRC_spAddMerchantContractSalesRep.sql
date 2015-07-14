drop procedure if exists avz_MRC_spAddMerchantContractSalesRep;

delimiter $$

create procedure avz_MRC_spAddMerchantContractSalesRep
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

Insert into tb_contractsalesrep(IsPrimary,contractId,salesRepId,CreatedUserId,CreationDate)
Select iNIsPrimarySalesRep,iNcontractId,iNSalesRepId,iNUserId,now();

Update tb_contracts set SalesRepID=LAST_INSERT_ID() where contractid=iNcontractId;
end;
else
begin
Insert into tb_contractsalesrep(IsPrimary,contractId,salesRepId,CreatedUserId,CreationDate)
Select iNIsPrimarySalesRep,iNcontractId,iNSalesRepId,iNUserId,now();

Update tb_contracts set SalesRepID=LAST_INSERT_ID() where contractid=iNcontractId;
end;
end if;
commit;
end;