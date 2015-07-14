drop procedure if exists avz_MRC_spUpdateMerchantBusinessInformation;

delimiter $$

create procedure avz_MRC_spUpdateMerchantBusinessInformation
(
iNmerchantId bigint,iNLegalName varchar(200),iNBusinessName varchar(200),iNBusinessStatus varchar(200),iNEntityTypeID int,
iNRNCNumber varchar(11),iNbusinessStartDate date,iNbusinessCCStartDate date,iNIndustryTypeID int,iNAffiliationNumber varchar(11),
iNPAddress varchar(200),iNPprovinceId int,iNPPhone1 varchar(20),iNPPhone2 varchar(20),iNPemail varchar(50),iNPFax varchar(20),
iNLAddress varchar(200),iNLprovinceId int,iNLPhone1 varchar(20),iNLPhone2 varchar(20),iNLemail varchar(50),iNLFax varchar(20),
iNLCity varchar(50),iNUserId bigint
)
begin

declare AID1, AID2 int;
declare AID3 int;
Select distinct AddressId into AID1  from tb_merchants Where merchantId=iNmerchantId;

select distinct AddressId2 into AID3  from tb_merchants Where merchantId=iNmerchantId;

Select distinct m.AddressId into AID2 from tb_merchants m
Inner Join tb_contracts as CNT on CNT.MerchantId=m.MerchantId
left join tb_addresses ad on m.AddressId=ad.AddressId
left join tb_documents td on m.MerchantId=td.MerchantId
where m.MerchantId=iNmerchantId and CNT.ContractId=avz_gen_fnRetrieveActiveContract(m.MerchantId) and td.DocumentTypeId =3; 


Update tb_merchants set LegalName=iNLegalName,BusinessName=iNBusinessName,BusinessTypeId=iNEntityTypeID,RNCNumber=iNRNCNumber,
businessStartDate=iNbusinessStartDate,IndustryTypeID=iNIndustryTypeID,ModifyUserId=iNUserId,ModifyDate=now() where merchantId=iNmerchantId;

Update tb_processor set firstProcessedDate=iNbusinessCCStartDate Where merchantId=iNmerchantId;

Update tb_addresses set Address1=iNPAddress,Phone1=iNPPhone1,Phone2=iNPPhone2,email1=iNPemail,FaxNum=iNPFax,
State=iNPprovinceId,ModifyUserId=iNUserId,ModifyDate=now() where AddressID=AID1;

if(AID3 is null) then
begin
insert into tb_addresses (Address1,Phone1,Phone2,email1,FaxNum,State,city,ModifyUserId,ModifyDate)
values(iNLAddress,iNLPhone1,iNLPhone2,iNLemail,iNLFax,iNLprovinceId,iNLCity,iNUserId,now());

update tb_merchants set AddressId2 =last_insert_id() where MerchantId=iNmerchantId;

end;
else
begin
Update tb_addresses set Address1=iNLAddress,Phone1=iNLPhone1,Phone2=iNLPhone2,email1=iNLemail,FaxNum=iNLFax,
State=iNLprovinceId,city=iNLCity,ModifyUserId=iNUserId,ModifyDate=now() where AddressID=AID3;
end;
end if;


end;