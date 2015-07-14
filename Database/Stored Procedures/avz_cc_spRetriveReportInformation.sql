drop procedure if exists avz_cc_spRetriveReportInformation;

delimiter $$
create procedure avz_cc_spRetriveReportInformation(iNmerchantid bigint,iNcontractId bigint)

SELECT      ct.PassportNbr AS ownerId, rncnumber AS rnc,ifnull(mrc.avgmccv,0) as mccv,
    scr.score,scr.scorerodendo,scr.rawdata,scr.scoreletter as finalletter,scr.scorerodendo as roundedscore
    
FROM
    tb_merchants mrc
        JOIN
    tb_owners ow ON mrc.merchantId = ow.MerchantId
        JOIN
    tb_contracts cnt ON cnt.MerchantId = mrc.MerchantId
	left join
	tb_contacts ct on ct.ContactId=ow.ContactId
    left join 
    tb_scorecard scr 
    on scr.merchantid=mrc.MerchantId 
WHERE
    mrc.MerchantId = iNmerchantid and cnt.contractId =iNcontractId
order by scr.scorecardid desc
    limit 1 ;




