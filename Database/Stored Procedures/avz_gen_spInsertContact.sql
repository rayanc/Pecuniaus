drop procedure if exists avz_gen_spInsertContact;
delimiter  $$

create procedure avz_gen_spInsertContact(
inout iNcontactId bigint,
iNcontactTypeId int,
iNjobTitle varchar(200),
iNfirstName varchar(200),
iNmiddleName varchar(5),
iNlastName varchar(200),
iNaddressId bigint,
iNdateofBirth datetime,
iNssn varchar(10),
iNmerchantId bigint,
iNhomePhone varchar(20),
iNcellPhone varchar(20),
iNsfid varchar(200)
)

begin
if iNcontactId=0 then 
begin
INSERT INTO `tb_contacts`
(`ContactTypeId`,
`JobTitle`,
`FirstName`,
`MiddleName`,
`LastName`,
AddressId1,
`DateOfbirth`,
`SSN`,
`merchantid`,
HomePhone,CellPhone,sfid
)
VALUES
( iNcontactTypeId,
iNjobTitle,
iNfirstName,
iNmiddleName,
iNlastName,
iNaddressId,
iNdateofBirth,
iNssn,iNmerchantId,
iNhomePhone,
iNcellPhone,iNsfid

);
end;
else
begin
update `tb_contacts`
set 
`ContactTypeId`=iNcontactTypeId,
`JobTitle`=iNjobTitle,
`FirstName`=iNfirstName,
`MiddleName`=iNmiddleName,
`LastName`=iNlastName,
AddressId1=iNaddressId,
`DateOfbirth`=iNdateofBirth,
`SSN`=iNssn,
HomePhone=iNhomePhone,
CellPhone=iNcellPhone

where contactid=iNcontactId;
end;
end if;


end $$
 delimiter $$