﻿--Solutions
--Sql Server 2014 Express Edition
--1
--Выдать все контракты, заключенные в 2016 году, у которых нет ни одной оплаты (prm_collected не заполнен). 
select c.cnt_id
      ,c.cnt_number
from tbl_contract c left join tbl_premium p 
                           on c.cnt_id = p.prm_contract
where datepart( year, c.cnt_date ) = 2016
group by c.cnt_id
        ,c.cnt_number
having( count( p.prm_collected ) = 0 )

--
select c.cnt_id
      ,c.cnt_number
from tbl_contract c
where datepart( year, c.cnt_date ) = 2016
  and c.cnt_id not in ( select p.prm_contract 
                          from tbl_premium p
                         where p.prm_collected is not null )
--2
--Для всех контрактов, у которых нет ни одной начисленной премии (нет записей в таблице tbl_premium), добавить по одной записи в таблицу премий.
insert into tbl_premium ( prm_contract
                         ,prm_type
                         ,prm_due
                         ,prm_collected )
  select c.cnt_id
        ,null
        ,null
        ,null
   from tbl_contract c left join tbl_premium p 
                              on c.cnt_id = p.prm_contract
  where p.prm_id is null
--3
--Для контрактов с номерами ‘A’, ‘B’ и ‘C’ проставить оплаченную премию, равную начисленной.
update p
set p.prm_collected = p.prm_due
from tbl_premium p inner join tbl_contract c 
                           on c.cnt_id = p.prm_contract
where p.prm_type is null --премия
  and c.cnt_number in ( 'A', 'B', 'C' )
--4
--Для контрактов с номерами ‘A’, ‘B’ и ‘C’ удалить последний возврат.
with t( c_id
       ,p_id ) as (
                    select c.cnt_id
                          ,max( prm_id ) as prm_id
                      from tbl_premium p inner join tbl_contract c 
                                                 on c.cnt_id = p.prm_id
                     where c.cnt_number in ( 'A', 'B', 'C' )
                       and p.prm_type = 'R'
                  group by c.cnt_id )
delete p
from tbl_premium p inner join t
                           on p.prm_contract = t.c_id
                          and p.prm_id = t.p_id 
--
delete p
from tbl_premium p inner join (
                                  select c.cnt_id
                                        ,max( prm_id ) as prm_id
                                    from tbl_premium p inner join tbl_contract c 
                                                               on c.cnt_id = p.prm_id
                                   where c.cnt_number in ( 'A', 'B', 'C' )
                                     and p.prm_type = 'R'
                                group by c.cnt_id )t
                           on p.prm_contract = t.c_id
                          and p.prm_id = t.p_id 


