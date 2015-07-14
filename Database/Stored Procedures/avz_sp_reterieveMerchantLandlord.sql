drop procedure if exists avz_sp_reterieveMerchantLandlord;
delimiter $$
create procedure avz_sp_reterieveMerchantLandlord(
	iNmerchantId bigint
)
begin

select mrc.LegalName, 
	mrc.BusinessName, 
	concat(scont.FirstName, ' ' , scont.lastname) as SalesRepName,
	/*concat(ocont.FirstName, ' ' , ocont.lastname) as OwnerName, */
	ocont.FirstName as OwnerFirstName, 
	ocont.lastname as OwnerLastName, 
	own.IsAuthorized, 
	off.Retention, 
	cnt.LoanedAmount, 
	cnt.ownedAmount,
	ocont.PassportNbr, 
	mrc.BusinessStartDate, 
	mrc.GrossAnnualSales, 
	case ifnull(cnt.AdministrativeExpenses,0) 
			when 0 then (select ExpenseAmount from lkp_tb_expenses where cnt.LoanedAmount between MinimumFundingAmount and MaximumFundingAmount limit 1)
	else
			cnt.AdministrativeExpenses 
	end as AdminExp,		  
	mrc.MerchantId,
	cnt.TypeOfAdvances as TypeOfAdvanceId,
	own.OwnerId,
	mrc.RentFlag,
	cnt.FundingDate as ContractStartDate,
	cnt.WrittenOffDate as ContractExpireDate

from tb_merchants mrc
left join tb_owners own on (own.merchantId=mrc.MerchantId and own.IsAuthorized=1)
left join tb_contacts ocont on own.Contactid=ocont.ContactId
left join tb_salesrep sep on sep.salesRepId=mrc.salesRepId
left join tb_contacts scont on sep.Contactid=scont.ContactId
left join tb_contracts cnt on cnt.merchantId=mrc.MerchantId
left join tb_offers off on (cnt.ContractId=off.ContractId and off.status=190001)
left join lkp_tb_expenses exp on exp.ExpenceId=cnt.ExpensesId
where mrc.MerchantId=iNmerchantId limit 1;

/*
SELECT typ.TypeId, typ.Description FROM lkp_tb_types typ where typ.Usage='ADV';
*/
select mrcl.BuildingName as LLCompany, 
	mrc.BusinessName, 
	concat(scont.FirstName, ' ' , scont.lastname) as SalesRepName,
	mlcont.FirstName as LLFirstName, 
	mlcont.lastname as LLLastName,
	/*concat(mlcont.FirstName, ' ' , mlcont.lastname) as LandlordName,*/
	adr.Address1 as MerchantAddress, 
	mladr.Address1 as LandlordAddress, 
	mrc.RentedAmount
from tb_merchants mrc
left join tb_salesrep sep on sep.salesRepId=mrc.salesRepId
left join tb_addresses adr on adr.AddressId=mrc.AddressId
left join tb_contacts scont on sep.Contactid=scont.ContactId
left join tb_merchantlandlords mrcl on mrcl.MerchantId=mrc.MerchantId
left join tb_contacts mlcont on mlcont.Contactid=mrcl.ContactId
left join tb_addresses mladr on mladr.AddressId=mlcont.AddressId1
where mrc.MerchantId=iNmerchantId limit 1;

end;
