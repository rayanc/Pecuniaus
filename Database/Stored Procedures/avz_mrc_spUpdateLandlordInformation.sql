drop procedure if exists avz_mrc_spUpdateLandlordInformation;
delimiter $$
create procedure avz_mrc_spUpdateLandlordInformation(iNprovidedXml text ,iNmerchantId bigint)

BEGIN

declare contid bigint;
declare addid bigint;

IF exists(select 1 from tb_merchantlandlords where merchantId=iNmerchantId ) = 1 then 

select  ContactId into contid from tb_merchantlandlords where MerchantId=iNmerchantId;
select AddressId1 into addid from tb_contacts where ContactId=contid;

update tb_addresses set Phone1= coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mphonenumber'), ''),'') 
where AddressId=addid;

update tb_contacts set
FirstName=coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mfirstname'), ''),'') ,
LastName=coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mlastname'), ''),'')
where ContactId=contid; 

update tb_merchantlandlords set BuildingName= coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mcompanyname'), ''),'')
 where merchantId=iNmerchantId;

else

insert into tb_addresses (Phone1) values( coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mphonenumber'), ''),'') );
set addid= LAST_INSERT_ID();

insert into tb_contacts(FirstName,LastName,AddressId1) values(	
coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mfirstname'), ''),'') ,
coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mlastname'), ''),''),addid) ;

set contid=LAST_INSERT_ID();

insert into tb_merchantlandlords (ContactID, BuildingName, merchantId)values(contid,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mcompanyname'), ''),'')
,iNmerchantId);
end if;


end;
