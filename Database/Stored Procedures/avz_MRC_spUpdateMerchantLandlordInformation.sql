drop procedure if exists avz_MRC_spUpdateMerchantLandlordInformation;

delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `avz_MRC_spUpdateMerchantLandlordInformation`(
iNmerchantId bigint,
iNTypeofPropertyId varchar(200),
iNLandlordCompanyName varchar(200),
iNLandlordFirstName varchar(200),
iNLandlordLastName varchar(200),
iNRentedAmount decimal,
iNAddress varchar(200),
iNCity varchar(200),
iNPhone varchar(20),
iNCellPhone varchar(20),
iNEmail varchar(200),
iNUserId bigint
)
begin
declare AID, CID int;

select CNT.AddressId1 INTO AID
from tb_merchants MRC
inner join tb_merchantlandlords LL on LL.MerchantId=MRC.MerchantId
inner join tb_contacts CNT on CNT.ContactId=LL.ContactId
where MRC.MerchantId=iNmerchantId;

select CNT.ContactId INTO CID
from tb_merchants MRC
inner join tb_merchantlandlords LL on LL.MerchantId=MRC.MerchantId
inner join tb_contacts CNT on CNT.ContactId=LL.ContactId
where MRC.MerchantId=iNmerchantId;

start transaction;
update tb_merchants set 
RentedAmount=iNRentedAmount,RentFlag=iNTypeofPropertyId,LandlordName=iNLandlordCompanyName,ModifyUserId=iNUserId,ModifyDate=now()
where MerchantId=iNmerchantId;

Update tb_addresses set Address1=iNAddress,Phone1=iNPhone,Phone2=iNCellPhone,email1=iNemail,City=iNCity
,ModifyUserId=iNUserId,ModifyDate=now() where AddressID=AID;

Update tb_contacts set FirstName=iNLandlordFirstName, LastName=iNLandlordLastName Where ContactID=CID;
commit;
end