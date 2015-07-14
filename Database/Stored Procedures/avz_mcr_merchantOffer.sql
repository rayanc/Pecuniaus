drop procedure if exists avz_mcr_merchantOffer;
delimiter $$

create procedure avz_mcr_merchantOffer
(
	iNmerchantid bigint,
	iNcontractid bigint
)
begin

   select mrc.merchantid,
		LegalName as MerchantName,
		ent.Name as EntityType,
		mrc.RentFlag,
		RentedAmount,
		it.Name as IndustryType,
		mrc.businessStartDate,
        mrc.avgmccv
		from tb_merchants  mrc 
			left join  lkp_tb_entitytypes ent ON ent.EntitytypeID = mrc.BusinessTypeId 
			left join lkp_tb_industrytypes it on mrc.IndustryTypeId=it.industryTypeId
		where mrc.MerchantId = iNmerchantid;

	/*Merchant Adderss*/
	select ad.Address1,
		ad.Address2,
		ad.City,
		ad.Phone1,
		mcnt.CountryName,
        st.State
		  from tb_merchants  mrc 
		left join tb_addresses ad on mrc.AddressId=ad.AddressId
		left join lkp_tb_country mcnt on ad.Country = mcnt.CountryId 
        left join lkp_tb_province st on ad.State=st.StateID
		where MerchantId = iNmerchantid;

			
	/*Auth Owner Detals*/
	select 
		cnt.FirstName, cnt.LastName
		from tb_merchants  mrc 
		left join tb_owners owr on mrc.OwnerId= owr.OwnerId
			left join tb_contacts cnt on owr.ContactId=cnt.ContactId
		where mrc.MerchantId = iNmerchantid;  

	/*Auth Owner Adderss*/
	select ad.Address1,
		ad.Address2,
		ad.City,
		ad.Phone1,
		mcnt.CountryName,
        st.State
		  from tb_merchants  mrc 
		left join tb_addresses ad on mrc.AddressId=ad.AddressId
		left join lkp_tb_country mcnt on ad.Country = mcnt.CountryId 
	    left join lkp_tb_province st on ad.State=st.StateID
		where MerchantId = iNmerchantid;

	/*Contract*/
    
	select ContractId, st.Description Type from 	tb_contracts cnt
    left join lkp_tb_Types st on cnt.ContractTypeId=st.TypeID    
	where cnt.MerchantId=iNmerchantid and ContractId=iNcontractid;

	/*Offers */
    
	select LoanAmount, OwnedAmount, Retention, Turn from  tb_offers
	where MerchantId=iNmerchantid and ContractId=iNcontractid;

end;


