drop procedure if exists avz_sp_InsertMerchantTradReference;
delimiter $$
create procedure avz_sp_InsertMerchantTradReference
(merchantId bigint,
merchantidTmp bigint)

begin

declare _mComercialReference1,_mComercialReference2 varchar(250);
declare _mComercialReferenceTele1,_mComercialReferenceTele2 varchar(12);
select tradereferenceName1,tradereferencePhone1,tradereferenceName2,tradereferencePhone2 into 
_mComercialReference1,_mComercialReferenceTele1,_mComercialReference2,_mComercialReferenceTele2
from tmp_tb_tradereference where merchantid=merchantidTmp;

SET SQL_SAFE_UPDATES = 0;
IF EXISTS (
 SELECT merchantid FROM tb_merchanttradereference WHERE merchantid = merchantId limit 1)=0 THEN 

begin 
  insert into tb_merchanttradereference(tradereferenceName,TelePhone,tradereferenceName2,TelePhone2,merchantid)
  values(_mComercialReference1,_mComercialReferenceTele1,_mComercialReference2,_mComercialReferenceTele2,merchantId);
  end;
  else 
  begin
  update tb_merchanttradereference
  set 
  tradereferenceName=_mComercialReference1,
  TelePhone=_mComercialReferenceTele1,
  tradereferenceName2=_mComercialReference2,
 TelePhone2=_mComercialReferenceTele2
  where merchantid=merchantId;
  
  end;
  end if;
  end;