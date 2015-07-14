drop procedure if exists AVZ_spGetEmails;

delimiter $$
create Procedure AVZ_spGetEmails(
iNSalesRep bigint /* Merchant Id  */
)
Begin

select adr.email1 as Email from tb_salesrep sep
left join tb_merchants mrc on mrc.SalesRepId=sep.SalesRepId
left join tb_addresses adr on adr.AddressId=sep.AddressId
where mrc.MerchantId=iNSalesRep
union
select MemberEmail as Email from tb_emailgroupmembers where GroupId in(1,2,3);

end;