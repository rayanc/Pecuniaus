drop procedure if exists avz_sf_spCreateOfferfromSalesForce;
create procedure avz_sf_spCreateOfferfromSalesForce(iNaccountId varchar(200))
begin
select * from 
tb_contracts cnt
join 
tb_offers offr on 
cnt.contractid=offr.contractid;





end;
