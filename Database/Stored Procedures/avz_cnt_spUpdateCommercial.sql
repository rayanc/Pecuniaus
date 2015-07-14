drop procedure if exists avz_cnt_spUpdateCommercial;

Delimiter $$
Create procedure avz_cnt_spUpdateCommercial
(
	iNMerchantid bigint,
	iNbusinessName varchar(250),
	iNTelephone varchar(20),
	iNAddress varchar(250),
	iNStateId bigint,
	iNCity varchar(250),
	iNAddressId bigint
)
begin
update tb_merchants set BusinessName=iNbusinessName 
where MerchantId=iNMerchantid;

if(iNAddressId > 0) then
update tb_addresses set Address1=iNAddress, City=iNCity, State=iNStateId, Phone1=iNTelephone 
where AddressId=iNAddressId;

end if;
end;