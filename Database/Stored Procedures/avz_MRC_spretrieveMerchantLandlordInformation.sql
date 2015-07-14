
drop procedure if exists avz_MRC_spretrieveMerchantLandlordInformation;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `avz_MRC_spretrieveMerchantLandlordInformation`(iNmerchantId bigint)
begin
Select DISTINCT
MRC.MerchantId,MRCST.StatusName as MerchantStatus,CNTST.StatusName as ContractStatus,TYP.TypeId as TypeofPropertyId,
MRC.LandlordName as LandlordCompanyName,CNT.FirstName as LandlordName,
CNT.FirstName as LandlordFirstName , CNT.LastName as LandlordLastName,
MRC.RentedAmount,ADR1.Address1 as Address,
IF(ADR1.Phone1 Is Null,'0000',ADR1.Phone1) as Phone,IF(ADR1.Phone2 Is Null,'0000',ADR1.Phone2) as CellPhone,ADR1.email1 as Email,City
from tb_merchants MRC
inner join tb_merchantlandlords LL on LL.MerchantId=MRC.MerchantId
inner join tb_contacts CNT on CNT.ContactId=LL.ContactId and CNT.MerchantId=MRC.MerchantId
Inner Join tb_contracts as CNTR on CNTR.MerchantId=MRC.MerchantId
inner Join lkp_tb_statuses as MRCST on MRCST.StatusId=MRC.StatusId
inner Join lkp_tb_statuses as CNTST on CNTST.StatusId=CNTR.StatusId
Inner Join tb_addresses as ADR1 on CNT.AddressId1=ADR1.AddressId
Inner Join lkp_tb_types TYP on TYP.TypeId=MRC.RentFlag
where MRC.MerchantId=iNmerchantId and TYP.Usage='PTY' and CNTR.ContractId=avz_gen_fnRetrieveActiveContract(iNmerchantId);
end$$
DELIMITER ;
