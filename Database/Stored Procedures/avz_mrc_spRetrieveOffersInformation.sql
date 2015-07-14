drop procedure if exists avz_mrc_spRetrieveOffersInformation;
delimiter $$
create procedure avz_mrc_spRetrieveOffersInformation()
begin

SELECT DISTINCT
    IFNULL(sc.score, 0.0) AS score,
    IFNULL(ofr.retention, 0.0) AS retention,
    IFNULL(cnt.averagemccv, 0.0) AS avgcc,
    IFNULL(ofr.ownedamount, 0.0) AS loanamount,
    IFNULL(ofr.loanamount, 0.0) AS mcaamount,
    IFNULL(ofr.yearly, 0.0) AS yearlysales,
    IFNULL(ofr.offercreationdate, NOW()) AS offerdate,
    'None specified' AS reason,
    ofr.turn AS turn,
    'Active' AS stagename,
    ofr.expirationdate AS closedate,
    ofr.contractid AS contractid,
    mrc.accountid AS accountid,
    stat.statusname AS status,
    'OfferName' AS name,
    1 AS closedatespecified,
    ofr.OfferId,
    ofr.sfid AS offeraccountid,
    mrc.merchantid
FROM
    tb_merchants mrc
        JOIN
    tb_offers ofr ON mrc.merchantid = ofr.merchantid
        LEFT JOIN
    tb_scorecard sc ON sc.merchantid = mrc.merchantid
      left  JOIN
    tb_contracts cnt ON cnt.contractid = ofr.contractid
      left  JOIN
    lkp_tb_statuses stat ON cnt.statusid = stat.statusid
where ofr.issynced =0;

end;


