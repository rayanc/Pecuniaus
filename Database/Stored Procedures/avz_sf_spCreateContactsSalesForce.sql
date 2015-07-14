drop procedure if exists avz_sf_spCreateContactsSalesForce;
delimiter $$
create procedure avz_sf_spCreateContactsSalesForce
(
iNmerchantId bigint,
iNJobTitle varchar(200),
iNFirstName varchar(200),
iNLastName varchar(200),
iNisowner bit,
iNcellphone varchar(200),
iNhomephone varchar(200),
iNaddress1 varchar(200),
iNaddress2 varchar(200),
iNcountry varchar(200),
iNstate varchar(200),
iNcity varchar(200),
iNzipcode varchar(20),
iNemail varchar(200),
iNsfId varchar(200)
)
begin 
DECLARE _iOaddressId bigint default 0;
DECLARE _addressType bigint default 3;
DECLARE _province bigint default 0;

# GET THE STATE ID FOR THE STATES RECIEVED
SELECT 
    StateID
FROM
    lkp_tb_province
WHERE
    State = iNstate INTO _province;

# SET THE ADDRESS TYPE
if(iNisowner=1)
then 
set _addressType=2;
else
set _addressType=3;
end if;

#CHECK IF ANY RECORE MATCHES THE SALES FORCE ID
if exists(select 1 from tmp_tb_contacts where sfid=iNsfId)=0 then

INSERT INTO tmp_tb_contacts
(
merchantId,
JobTitle,
FirstName,
LastName,
isowner,
cellphone,
homephone,
sfId

)
values
(
iNmerchantId,
iNjobTitle,
iNfirstname,
iNlastName,
iNisOwner ,
iNhomePhone ,
iNcellPhone,
iNsfId );
else

UPDATE tmp_tb_contacts 
SET 
    JobTitle = iNjobTitle,
    FirstName = iNfirstname,
    LastName = iNlastName,
    isOwner = iNisOwner,
    homePhone = iNhomePhone,
    cellPhone = iNcellPhone,
    merchantId=iNmerchantid
WHERE
    sfid = iNsfId;
end if;

/*
Add the aacount in the main table if exists
*/
if exists(select 1 from tb_contacts where sfid=iNsfId)=0 then
call avz_gen_spInsertAddress(_iOaddressId,_addressType,iNaddress1,iNaddress2,
iNcountry,iNcity,_province,'',
iNhomePhone,iNcellPhone,'',iNemail,
'','',2,_iOaddressId);
#call avz_gen_spInsertAddress(_iOaddressId,3,_address,'',_country,_city,_province,'',_phone1,_phone2,'',_email,'','',2,_iOaddressId);
INSERT INTO tb_contacts
(
merchantId,
JobTitle,
FirstName,
LastName,
cellphone,
homephone,
sfId,
authorized,
AddressId1

)
values
(
iNmerchantId,
iNjobTitle,
iNfirstname,
iNlastName,
iNhomePhone ,
iNcellPhone,
iNsfId,iNisOwner,_iOaddressId);

else

select addressid1 into _iOaddressId from tb_contacts where sfid=iNsfId limit 1;

call avz_gen_spInsertAddress(_iOaddressId,_addressType,iNaddress1,iNaddress2,
iNcountry,iNcity,_province,'',iNhomePhone,
iNcellPhone,'',iNemail,'','',
2,_iOaddressId);

UPDATE tb_contacts 
SET 
    JobTitle = iNjobTitle,
    FirstName = iNfirstname,
    LastName = iNlastName,
    authorized = iNisOwner,
    homePhone = iNhomePhone,
    cellPhone = iNcellPhone,
    merchantId = iNmerchantid
WHERE
    sfid = iNsfId;
end if;
end;

