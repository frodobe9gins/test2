declare @t table(id int,ClientName nvarchar(100),SupplyNumber int, [Date] date, Amount decimal )


insert into @t  select 1,'ООО Ромашка',111,'2017-01-01',100
insert into @t  select 2,'ООО Ромашка',222,'2017-01-05',150
insert into @t  select 3,'ООО Ромашка',333,'2017-02-07',200
insert into @t  select 4,'ИП Лютик',444,'2017-01-02',110
insert into @t  select 5,'ИП Лютик',555,'2017-04-05',120
insert into @t  select 6,'ИП Лютик',666,'2017-04-07',210
insert into @t  select 7,'ООО Ромашка',777,'2018-03-10',100
insert into @t  select 8,'ИП Лютик',888,'2018-04-12',210
insert into @t  select 10,'ЗАО Тюльпан',1010,'2017-06-06',1000

	select ClientName, DATEPART(MM, [Date]) as [Month],sum(Amount) as SumAmount from @t t 
	where  t.[Date]<'2018-01-01' and t.[Date]>='2017-01-01'  
	group by ClientName, DATEPART(MM, [Date])
	order by ClientName,[Month],SumAmount 