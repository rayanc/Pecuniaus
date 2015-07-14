call avz_MRC_spretrieveMerchantBusinessInformation(145);

drop procedure if exists avz_MRC_spretrieveMerchantBusinessInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantBusinessInformation(iNmerchantId bigint)
begin

Select distinct
MRC.MerchantId,MRCST.StatusName as MerchantStatus,CNTST.StatusName as ContractStatus,MRC.LegalName,MRC.BusinessName,'Missing' as BusinessStatus,
ifnull(CNT.LoanedAmount,0) as LoanedAmount,ifnull(CNT.OwnedAmount,0) as OwnedAmount, ifnull(CNT.PaidAmount,0) as PaidAmount,CNT.ContractId,
BusinessTypeId as EntityTypeID,MRC.RNCNumber,DATE_FORMAT(businessStartDate, '%Y-%m-%d') businessStartDate,(Select DATE_FORMAT(Min(firstProcessedDate), '%Y-%m-%d') as businessCCStartDate from tb_processor Where MerchantId=MRC.MerchantId) as businessCCStartDate,
INTYP.Name IndustryType,MRC.IndustryTypeID,If(AFL.AffiliationNumber Is null,'',AFL.AffiliationNumber) as AffiliationNumber,
ADR1.Address1 as PAddress,PRV1.StateID as PprovinceId,ADR1.Phone1 as PPhone1,ADR1.Phone2 as PPhone2,ADR1.email1 as Pemail,ADR1.FaxNum as PFax,
ifnull(ADR2.Address1,ADR1.Address1) as LAddress,ifnull(PRV2.StateID,ifnull(PRV1.StateID,0)) as LprovinceId,
ifnull(ADR2.Phone1,ADR1.Phone1) as LPhone1, ifnull(ADR2.Phone2,ADR1.Phone2) as LPhone2,ifnull(ADR2.email1,ADR1.email1) as Lemail,
ifnull(ADR2.FaxNum,ADR1.FaxNum) as LFax,ifnull(ADR2.City,ADR1.City) as LCity
from tb_merchants as MRC
Inner Join tb_contracts as CNT on CNT.MerchantId=MRC.MerchantId
Inner Join tb_addresses as ADR1 on ADR1.AddressID=MRC.AddressId
left Join tb_addresses as ADR2 on ADR2.AddressId=MRC.AddressId2
left join (select MerchantId from tb_documents d where DocumentTypeId =3) td on MRC.MerchantId=td.MerchantId
inner Join lkp_tb_statuses as MRCST on MRCST.StatusId=MRC.StatusId
inner Join lkp_tb_statuses as CNTST on CNTST.StatusId=CNT.StatusId
Inner Join lkp_tb_province PRV1 on PRV1.StateID=ADR1.State
Left Join lkp_tb_province PRV2 on PRV2.StateID=ADR2.State
left join tb_merchantaffiliations AFL on AFL.MerchantID=MRC.MerchantId
inner join lkp_tb_industrytypes INTYP on MRC.IndustryTypeID=INTYP.IndustryTypeID
Where MRC.merchantId=iNmerchantId and CNT.contractId=avz_gen_fnRetrieveActiveContract(MRC.MerchantId);
end;