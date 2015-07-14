drop procedure if exists avz_cc_spRetrieveMerchantCreditInformation;
delimiter $$
create procedure avz_cc_spRetrieveMerchantCreditInformation
( 
iNmerchantId bigint,
iNcontractId bigint
)

begin 
SELECT 
    mrc.businessName,
    ifnull(mrc.grossAnnualSales,0.0) as grossAnnualSales,
    adr.address1,
    adr.address2,
    adr.country,
    prov.state,
    adr.city,
    ifnull(scr.score,0.0) as score,	
	scr.scoreletter as finalscore,
    mrc.avgmccv AS averagemccv,
	(select MaxTime from tb_offerConfiguration where score=scr.scoreletter limit 1) as maxprice,
	typ.SalesTaken as salestaken,
	ifnull((select Reason from tb_merchanthistory where merchantId=iNmerchantId and field='MCCV' order by 1 desc limit 1),'') as reason,
    cnt.IsOffersSent,
	cnt.ContractID
FROM
    tb_merchants mrc
        LEFT JOIN
    tb_addresses adr ON mrc.addressId = adr.addressId
        LEFT JOIN
    lkp_tb_province prov ON adr.state = prov.StateID
        LEFT JOIN
    tb_scorecard scr ON mrc.merchantId = scr.merchantId
	        LEFT JOIN
    tb_contracts cnt ON mrc.merchantId = cnt.MerchantId
			Left JOIN
	lkp_tb_industrytypes typ on mrc.IndustryTypeId=typ.IndustryTypeId

WHERE
    mrc.merchantid = iNmerchantId And 
	cnt.ContractID = case when iNcontractId > 0 then iNcontractId else cnt.ContractID End
 order by scr.scorecardid desc limit 1;
    
SELECT 
    CONCAT(cont.FirstName, ' ', cont.LastName) AS name,
    adr.email1 AS email
FROM
    tb_salesrep sal
        LEFT JOIN
    tb_contractsalesrep cntsal ON sal.salesrepId = cntsal.contractSalesRepId
        LEFT JOIN
    tb_contacts cont ON sal.contactid = cont.contactId
        LEFT JOIN
    tb_addresses adr ON adr.addressId = sal.addressId
        LEFT JOIN
    tb_contracts cnt ON cnt.contractid = cntsal.contractid
        LEFT JOIN
    tb_merchants mrc ON cnt.merchantId = mrc.merchantId
/*WHERE
    mrc.merchantid = iNmerchantId*/;

select score,MaxTime,Minprice from tb_offerminprice where score =(select scr.scoreletter from tb_scorecard scr where 
scr.merchantId =iNmerchantId order by scr.scorecardid desc limit 1);

end;

