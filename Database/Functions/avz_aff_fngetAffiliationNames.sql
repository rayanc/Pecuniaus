drop function if exists avz_aff_fngetAffiliationNames;
Delimiter //

Create function avz_aff_fngetAffiliationNames(iNMerchantid bigint) returns varchar(200)
Begin

Declare aff varchar(200);

Select replace(GROUP_CONCAT(LegalName),',',';')  affiliations into aff 
from (
Select AM.LegalName from tb_merchantaffiliations A JOIN tb_merchants M on A.RNCNumber=M.RNCNumber
JOIN tb_merchants AM on A.MerchantID=AM.MerchantId
where M.MerchantId=iNMerchantid
#
UNION

Select M.LegalName from tb_merchantaffiliations A JOIN tb_contacts C on A.OwnerID=C.PassportNbr
JOIN tb_owners O on C.ContactId=O.ContactId and O.IsAuthorized=1
JOIN tb_merchants M on A.MerchantId=M.MerchantId
where O.merchantId=iNMerchantid
) as Affiliations;

return (aff);

end //

Delimiter ;


#select avz_aff_fngetAffiliationNames(118)