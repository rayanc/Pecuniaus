drop procedure if exists avz_sal_spRetrieveSalesRep;
delimiter $$

create procedure avz_sal_spRetrieveSalesRep
( iNSearchString varchar(200) )
begin 
declare searchstring longtext;
declare conditions longtext;
set @searchstring='select 
firstName,jobTitle,lastName,salesRepId,SSN , concat(firstName,'' '',lastName) AS fullName, UserId
from tb_salesrep sal
join tb_contacts salc
on sal.contactid=salc.contactid
where  1=1  ';

if(iNSearchString!='') then
 set @searchstring=  concat(@searchstring,'  and', ' salc.firstname ', ' like ','''%', iNSearchString,'%''','  or', ' salc.LastName ', ' like ','''%', iNSearchString,'%''');
  end if;

 PREPARE stmt FROM @searchstring;
 
 EXECUTE stmt;
 DEALLOCATE PREPARE stmt;

end $$
delimiter $$

call avz_sal_spRetrieveSalesRep('');