--DB SCHEMA
PRINT N'Recreating the objects for the database'
--Drop all FKs in the database
declare @table_name sysname, @constraint_name sysname
declare i cursor static for 
select c.table_name
      ,a.constraint_name
from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS a join INFORMATION_SCHEMA.KEY_COLUMN_USAGE b
                                                    on a.unique_constraint_name=b.constraint_name 
                                                  join INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
                                                    on a.constraint_name=c.constraint_name
where upper( c.table_name ) IN ( upper( 'tbl_contract' ), upper( 'tbl_premium' ) )
open i
  fetch next from i into @table_name,@constraint_name
  while @@fetch_status=0
    begin
	  exec('ALTER TABLE '+@table_name+' DROP CONSTRAINT '+@constraint_name)
	  fetch next from i into @table_name,@constraint_name
    end
close i
deallocate i
GO
--Drop all tables
declare @object_name sysname
       ,@sql varchar(8000)
declare i cursor static for 
                            select table_name 
                              from INFORMATION_SCHEMA.TABLES
                             where upper( table_name ) in ( upper( 'tbl_contract' ), upper( 'tbl_premium' ) )
open i
  fetch next from i into @object_name
  while @@fetch_status=0
    begin
	  set @sql='DROP TABLE [dbo].['+@object_name+']'
	  exec(@sql)
	  fetch next from i into @object_name
    end
close i
deallocate i
GO

create table [dbo].[tbl_contract] (
	[cnt_id] [int] IDENTITY(1,1) primary key,
	[cnt_number] [varchar] (50) not null,
	[cnt_date] [date] not null
) on [primary]

GO

create table [dbo].[tbl_premium] (
	[prm_id] [int] IDENTITY(1,1) primary key,
	[prm_contract] [int] not null,
	[prm_type] [varchar] (1) null,
    [prm_due] [int] null,
    [prm_collected] [int] null,
) on [primary]
GO

alter table [dbo].[tbl_premium] ADD 
	CONSTRAINT [fk_key] FOREIGN KEY 
	(
		[prm_contract]
	) REFERENCES [dbo].[tbl_contract ] (
		[cnt_id]
	)


