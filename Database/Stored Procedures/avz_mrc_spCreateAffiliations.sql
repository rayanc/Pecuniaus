call avz_mrc_spCreateAffiliations(18);
drop procedure if exists avz_mrc_spCreateAffiliations;
delimiter $$

create procedure avz_mrc_spCreateAffiliations(iNmerchantId bigint)
begin
declare _rncnumber varchar(200) default '';
declare _ownerid bigint default 0;
/*
Check if there is already an affiliation of the merchant
*/

SELECT 
    RNCNumber, ownerid
INTO _rncnumber , _ownerid FROM
    tb_merchants
WHERE
    merchantid = iNmerchantId;
create temporary table affiliations
(
merchantid bigint,
rncnumber varchar(200),
ownerid bigint);

insert into affiliations(merchantid,rncnumber,ownerid)


Select merchantid,RNCNumber,OwnerId from tb_merchants where ownerid=_ownerid group by ownerid having count(ownerid)>1
union
Select merchantid,RNCNumber,ownerid from tb_merchants where RNCNumber=_rncnumber group by ownerid having count(RNCNumber)>1;

insert into tb_merchantaffiliations(merchantid,rncnumber,ownerid)

select merchantid,RNCNumber,OwnerId  from affiliations on duplicate key
 update merchantid=merchantid,rncnumber=rncnumber, ownerid=ownerid;

drop table affiliations;

end;
