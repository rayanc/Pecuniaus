drop procedure avz_sf_spRetrieveAccountstoUpdateSalesForce;
delimiter $$

CREATE  PROCEDURE `avz_sf_spRetrieveAccountstoUpdateSalesForce`()
BEGIN
/*SELECT  `companyId`,`legalName`,`businessName`,`salesRepId`,`businessStartDate`,`industryTypeId`,`businessTypeId`,`insertUserId`,`insertDate`,
`rncNumber`,`cnetProcessorNbr`,`vnetProcessorNbr`,`telePhone1`,`telePhone2`,`city`,`province`,`country`,`businessType`,`industryType`,`accountId`FROM `avz`.`tmp_tb_merchants`;
*/

SELECT 
    mrc.companyId,
    mrc.legalName,
    mrc.businessName,
    mrc.salesRepId,
    mrc.businessStartDate,
    mrc.industryTypeId,
    mrc.businessTypeId,
    mrc.insertUserId,
    mrc.insertDate,
    mrc.rncNumber,
    mrc.CNetProcessorId,
    mrc.VNetProcessoIdd,
    mrc.telePhone1,
    mrc.telePhone2,
    adr.city,
    adr.country,
    prov.state,
    adr.address1 as address1,
    adr.address2 as address2,
   mrc.BusinessWebSite as businessWebSite,
    ent.EntitytypeID as businessType,
    ind.Name as industryType,
    mrc.accountId,
    stats.statusname as merchantStatus,
    mrc.MerchantId as merchantId
FROM
    tb_merchants mrc
        JOIN
        lkp_tb_statuses stats
	on mrc.statusid=stats.statusid join
    tb_addresses adr ON mrc.AddressId = adr.AddressId
     left   JOIN
    lkp_tb_entitytypes ent ON mrc.IndustryTypeId = ent.EntitytypeID
    join lkp_tb_province prov on adr.state=prov.stateid
    join lkp_tb_industrytypes ind on mrc.IndustryTypeId=ind.IndustryTypeId
WHERE     mrc.isynced = 0;

/*
Get the list of contacts to be updated with salesforce id
*/

SELECT 
    cnt.ContactId,
    cnt.FirstName AS firstname,
    cnt.LastName AS lastname,
    cnt.HomePhone AS homephone,
    cnt.CellPhone AS cellphone,
    cnt.JobTitle AS jobtitle,
    cnt.sfid AS accountid,
	cnt.DateOfbirth as dateofBirth,
	(cnt.ContactTypeId = 3) as isowner,
	A.Address1 as Address1,
	A.Address2 as address2,
	A.City as city,
	A.State as state,
	A.Country as country,
	m.accountId as maccountId
FROM
    tb_owners own
        JOIN
    tb_contacts cnt ON own.ContactId = cnt.ContactId
	JOIN tb_addresses A ON cnt.AddressId1=A.AddressId
	JOIN tb_merchants m on cnt.merchantId=m.merchantId where m.isynced =0;

END