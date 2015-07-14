drop procedure if exists avz_mrc_spAddManualMCCV ;
delimiter $$
create procedure avz_mrc_spAddManualMCCV(iNmerchantId bigint,iNannualSales double, iNavgmccv double,iNreason text,iNcontractId bigint)
begin
declare _oldannualsales double default 0.0;
declare _oldmccv double default 0.0;
SELECT 
    GrossAnnualSales
INTO _oldannualsales FROM
    tb_merchants
WHERE
    merchantId = iNmerchantId;



select avgmccv into _oldmccv from tb_merchants where merchantId = iNmerchantId;

UPDATE tb_merchants 
SET 
    GrossAnnualSales = iNannualSales,
	avgmccv=iNavgmccv
WHERE
    merchantId = iNmerchantId;

if(_oldannualsales!=iNannualSales ) then
		INSERT INTO tb_merchanthistory(MerchantId,HistoryDate,Field,OldValue,NewValue,Reason) VALUES 
		(iNmerchantId,now(),'annualsales',_oldannualsales,iNannualSales,'');
		
end if;
if(_oldmccv!=iNavgmccv) then
		INSERT INTO tb_merchanthistory(MerchantId,HistoryDate,Field,OldValue,NewValue,Reason) VALUES 
		(iNmerchantId,now(),'mccv',_oldmccv,iNavgmccv,iNreason);
end if;
end;
