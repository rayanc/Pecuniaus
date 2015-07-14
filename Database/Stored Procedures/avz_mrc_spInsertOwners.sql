#CALL avz_mrc_spInsertOwners(144,62,159);
drop procedure if exists avz_mrc_spInsertOwners; 
delimiter $$ 
create procedure avz_mrc_spInsertOwners(iNmerchantIdtemp bigint,iNmerchantId bigint,iNaddressId bigint)

begin 
DECLARE _jobTitle,_firstName,_lastName,_middleName,_dateofBirth,_accountId varchar(200);
DECLARE _homephone,_cellphone,_ssn VARCHAR(20);
DECLARE _contactTypeId INTEGER;
DECLARE _contactid bigint default 0;
DECLARE _isowner bigint default 0;

DECLARE _finished INTEGER DEFAULT 0;
DECLARE Owners CURSOR FOR 
SELECT 
    contactTypeId,
    jobTitle,
    firstName,
    middleName,
    lastName,
    dateofBirth,
    ssn,
	homephone,
	cellphone,
	sfid,IsOwner
 FROM
    tmp_tb_contacts
WHERE
    merchantid = iNmerchantIdtemp; 

DECLARE CONTINUE HANDLER FOR NOT FOUND SET _finished = 1;

OPEN Owners;

GETDATA: loop
fetch Owners into _contactTypeId,_jobTitle,_firstName,_middleName,_lastName,_dateofBirth,_ssn,_homephone,_cellphone,_accountId,_isowner;

#IF NO DATA IS FOUND THEN QUIT
IF _finished = 1 THEN 
LEAVE GETDATA;
END IF;

CALL avz_gen_spinsertContact(_contactid,IFNULL(_contactTypeId,1), _jobTitle , _firstName , _middleName , _lastName , iNaddressId , _dateofBirth , _ssn,iNmerchantId,_homephone,_cellphone,_accountId);
if(_isowner=1) then 
select 'inside';
CALL `avz_mrc_spInsertowner`(0 ,iNmerchantId ,2 ,_contactid);
end if;
END LOOP GETDATA;

 CLOSE Owners;








end;