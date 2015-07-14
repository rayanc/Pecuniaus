drop procedure if exists avz_mrc_spinsertRecentlyVisted;
DELIMITER $$ 
create procedure avz_mrc_spinsertRecentlyVisted
(
iNmerchantId bigint,
iNuserId bigint
)

begin
if (SELECT EXISTS (select 1 from tb_recentlyvisited where merchantid=iNmerchantId and userId=iNuserId))=0 then
begin
INSERT INTO `tb_recentlyvisited`
(
merchantId,
userId,
datevisited
)
VALUES (iNmerchantId,iNuserId,now());
end;
else
begin
update tb_recentlyvisited  set datevisited=now() where merchantid=iNmerchantId and userId=iNuserId;
end;
end if;

end $$ 
delimiter $$ 