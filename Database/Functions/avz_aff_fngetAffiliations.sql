drop function if exists avz_aff_fngetAffiliationIDs;
Delimiter //

Create function avz_aff_fngetAffiliationIDs(iNMerchantid bigint) returns varchar(200)
Begin

Declare aff varchar(200);

Select replace(GROUP_CONCAT(affiliationid),',',';')  affiliations into aff from (
Select affiliationid from tb_merchantaffiliations A JOIN tb_merchants M on A.RNCNumber=M.RNCNumber
where M.MerchantId=iNMerchantid
#
UNION

Select affiliationid from tb_merchantaffiliations A JOIN tb_contacts C on A.OwnerID=C.PassportNbr
JOIN tb_owners O on C.ContactId=O.ContactId and O.IsAuthorized=1
where O.merchantId=iNMerchantid
) as Affiliations;

return (aff);

end //

Delimiter ;


#select avz_aff_fngetAffiliationIDs(118)