-- 清除GameCard表并重置自增ID
delete from GameCard;
update sqlite_sequence set seq = 0 where name = 'GameCard';

-- 为GameCard表增加一条数据
insert into GameCard (View,Type,Cost,CommandsJson) values ('CardView/foo',0,0,'');

-- 更新GameCard表一条数据
update GameCard 
set 
View = 'foo',Type='0' 
where
Id = 0;

-- 删除一条数据
delete from GameCard
where
Id = 0 and View='foo';