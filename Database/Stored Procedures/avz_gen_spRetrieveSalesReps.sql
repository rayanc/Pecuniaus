drop procedure if exists avz_gen_spRetrieveSalesReps;
delimiter $$
create procedure avz_gen_spRetrieveSalesReps()
begin
select 
SR.salesRepId keyId,CNT.FirstName description
from tb_salesrep SR
inner join tb_contacts CNT on CNT.contactId=SR.contactId;
end;