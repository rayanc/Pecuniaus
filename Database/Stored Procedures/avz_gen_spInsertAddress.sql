#CALL avz_gen_spInsertAddress(0,1,'','','','','','','','','','','','',2,@iOaddressId);
drop procedure if exists avz_gen_spInsertAddress;
#drop procedure avz_gen_spInsertAddress
delimiter $$

create procedure avz_gen_spInsertAddress
(
 pAddressId bigint,
pAddressTypeId smallint(6),
pAddress1 varchar(200),
pAddress2 varchar(200),
pCountry varchar(200),
pcity varchar(200),
pState varchar(020),
pZipCode varchar(10),
pPhone1 varchar(20),
pPhone2 varchar(20),
pPhone3 varchar(12),
pemail1 varchar(50),
pemail2 varchar(50),
pemail3 varchar(50),
pInsertUserId bigint(20),
OUT iOaddressId bigint
)
BEGIN
if pAddressId=0 then 
begin
 INSERT INTO tb_addresses (AddressTypeId,	Address1 ,Address2 ,Country ,city ,State ,ZipCode,Phone1,Phone2,Phone3,email1,email2 
								,email3,InsertUserId,InsertDate)
VALUES(	pAddressTypeId,pAddress1 ,pAddress2 ,pCountry ,pcity ,pState ,pZipCode,pPhone1,pPhone2,pPhone3,pemail1,pemail2 ,pemail3,pInsertUserId,
now());
set iOaddressId=last_insert_id();
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
	ZipCode=pZipCode,
	Phone1=pPhone1,
	Phone2=pPhone2,
	Phone3=pPhone3,
	email1=pemail1,
	email2=pemail2	,
	email3=pemail3,
    modifyuserID=pInsertUserId,
    modifydate=now()
	where addressId=pAddressId;
	end;
	end if;
end;
