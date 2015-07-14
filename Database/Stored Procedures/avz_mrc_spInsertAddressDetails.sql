-- ************************* INSERT Address DETAILS ***************************

DELIMITER $$
CREATE PROCEDURE `avz_mrc_spInsertAddressDetails`
(
pAddressId bigint,
pAddressTypeId smallint(6),
pAddress1 varchar(200),
pAddress2 varchar(200),
pCountry varchar(50),
pcity varchar(50),
pState varchar(50),
pZipCodeId int(11),
pPhone1 varchar(12),
pPhone2 varchar(12),
pPhone3 varchar(12),
pemail1 varchar(50),
pemail2 varchar(50),
pemail3 varchar(50),
pInsertUserId bigint(20)
)
BEGIN
if pAddressId!=0 then 
begin
 INSERT INTO tb_addresses (AddressTypeId,	Address1 ,Address2 ,Country ,city ,State ,ZipCodeId,Phone1,Phone2,Phone3,email1,email2 
								,email3,InsertUserId,InsertDate)
VALUES(	pAddressTypeId,
pAddress1 ,
pAddress2 ,
pCountry ,
pcity ,
pState ,
pZipCodeId,
pPhone1,
pPhone2,
pPhone3,
pemail1,
pemail2 ,
pemail3,
pInsertUserId,
now());
end;
else
begin
update tb_addresses
set AddressTypeId=pAddressTypeId,
	Address1=pAddress1 ,
	Address2= pAddress2,
	Country= pCountry,
	city=pcity ,
	State=pState ,
	ZipCodeId=pZipCodeId,
	Phone1=pPhone1,
	Phone2=pPhone2,
	Phone3=pPhone3,
	email1=pemail1,
	email2=pemail2	,
	email3=pemail3
	where addressId=pAddressId;
	end;
	end if;
END $$
DELIMITER ;
