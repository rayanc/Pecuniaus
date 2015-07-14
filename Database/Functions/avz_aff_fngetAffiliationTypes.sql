drop function if exists avz_aff_fngetAffiliationTypes;
Delimiter //

Create function avz_aff_fngetAffiliationTypes(iNMerchantid bigint) returns varchar(200)
Begin

Declare aff varchar(200);

Select replace(GROUP_CONCAT(Type),',',';')  affiliations into aff 
from (
Select 'RNC' as Type,AM.merchantid from tb_merchantaffiliations A JOIN tb_merchants M on A.RNCNumber=M.RNCNumber
JOIN tb_merchants AM on A.MerchantID=AM.MerchantId
where M.MerchantId=iNMerchantid
#
UNION

Select 'OWNERID' as Type,M.merchantid from tb_merchantaffiliations A JOIN tb_contacts C on A.OwnerID=C.PassportNbr
JOIN tb_owners O on C.ContactId=O.ContactId and O.IsAuthorized=1
JOIN tb_merchants M on A.MerchantId=M.MerchantId
where O.merchantId=iNMerchantid
) as Affiliations;

return (aff);

end //

Delimiter ;


#select avz_aff_fngetAffiliationTypes(118)