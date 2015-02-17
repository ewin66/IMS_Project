/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     14.02.2015 23:58:07                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Employee') and o.name = 'FK_Employee_Department')
alter table dbo.Employee
   drop constraint FK_Employee_Department
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('"Order"') and o.name = 'FK_ORDER_REFERENCE_CUSTOMER')
alter table "Order"
   drop constraint FK_ORDER_REFERENCE_CUSTOMER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('"Order"') and o.name = 'FK_ORDER_REFERENCE_EMPLOYEE')
alter table "Order"
   drop constraint FK_ORDER_REFERENCE_EMPLOYEE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('"Order"') and o.name = 'FK_ORDER_REFERENCE_ORDERSTA')
alter table "Order"
   drop constraint FK_ORDER_REFERENCE_ORDERSTA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDetails') and o.name = 'FK_ORDERDET_REFERENCE_PRODUCT')
alter table OrderDetails
   drop constraint FK_ORDERDET_REFERENCE_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderDetails') and o.name = 'FK_ORDERDET_REFERENCE_ORDER')
alter table OrderDetails
   drop constraint FK_ORDERDET_REFERENCE_ORDER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Product') and o.name = 'FK_PRODUCT_REFERENCE_CATEGORY')
alter table dbo.Product
   drop constraint FK_PRODUCT_REFERENCE_CATEGORY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Product') and o.name = 'FK_PRODUCT_REFERENCE_SUPLIER')
alter table dbo.Product
   drop constraint FK_PRODUCT_REFERENCE_SUPLIER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Articles_Source')
            and   type = 'U')
   drop table dbo.Articles_Source
go

alter table BarCode
   drop constraint PK_BARCODE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BarCode')
            and   type = 'U')
   drop table BarCode
go

alter table Category
   drop constraint PK_CATEGORY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Category')
            and   type = 'U')
   drop table Category
go

alter table dbo.Customer
   drop constraint PK_Customer
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Customer')
            and   type = 'U')
   drop table dbo.Customer
go

alter table dbo.Department
   drop constraint PK__Departme__B2079BEDA4F24D5C
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Department')
            and   type = 'U')
   drop table dbo.Department
go

alter table dbo.Employee
   drop constraint PK__Employee__7AD04F1160FEF479
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Employee')
            and   type = 'U')
   drop table dbo.Employee
go

alter table "Order"
   drop constraint PK_ORDER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"Order"')
            and   type = 'U')
   drop table "Order"
go

alter table OrderDetails
   drop constraint PK_ORDERDETAILS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderDetails')
            and   type = 'U')
   drop table OrderDetails
go

alter table OrderStatus
   drop constraint PK_ORDERSTATUS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderStatus')
            and   type = 'U')
   drop table OrderStatus
go

alter table dbo.Product
   drop constraint PK_PRODUCT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Product')
            and   type = 'U')
   drop table dbo.Product
go

alter table dbo.Promet
   drop constraint PK_Promet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Promet')
            and   type = 'U')
   drop table dbo.Promet
go

alter table Suplier
   drop constraint PK_SUPLIER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Suplier')
            and   type = 'U')
   drop table Suplier
go

/*==============================================================*/
/* Table: Articles_Source                                       */
/*==============================================================*/
create table dbo.Articles_Source (
   "Column 0"           varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 1"           nvarchar(50)         collate Macedonian_FYROM_90_CI_AS null,
   "Column 2"           decimal(28)          null,
   "Column 3"           varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 4"           varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 5"           varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 6"           varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 7"           varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 8"           varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 9"           varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 10"          varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 11"          varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 12"          varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 13"          varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 14"          varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 15"          varchar(50)          collate Macedonian_FYROM_90_CI_AS null,
   "Column 16"          varchar(50)          collate Macedonian_FYROM_90_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: BarCode                                               */
/*==============================================================*/
create table BarCode (
   BarcodeId            int                  not null,
   Barcode              varchar(13)          not null,
   Created              datetime             not null,
   Updated              datetime             not null
)
go

alter table BarCode
   add constraint PK_BARCODE primary key (BarcodeId)
go

/*==============================================================*/
/* Table: Category                                              */
/*==============================================================*/
create table Category (
   CategoryId           int                  identity,
   CategoryName         nvarchar(1000)       not null,
   Created              datetime             not null,
   Updated              datetime             not null
)
go

alter table Category
   add constraint PK_CATEGORY primary key (CategoryId)
go

/*==============================================================*/
/* Table: Customer                                              */
/*==============================================================*/
create table dbo.Customer (
   CustomerId           int                  identity,
   CustomerName         nvarchar(200)        collate Macedonian_FYROM_90_CI_AS not null
)
on "PRIMARY"
go

alter table dbo.Customer
   add constraint PK_Customer primary key (CustomerId)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Department                                            */
/*==============================================================*/
create table dbo.Department (
   DepartmentId         int                  identity(1, 1),
   Name                 varchar(50)          collate Macedonian_FYROM_90_CI_AS null
)
on "PRIMARY"
go

alter table dbo.Department
   add constraint PK__Departme__B2079BEDA4F24D5C primary key (DepartmentId)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Employee                                              */
/*==============================================================*/
create table dbo.Employee (
   EmployeeId           int                  identity(1, 1),
   DepartmentId         int                  not null,
   FirstName            varchar(20)          collate Macedonian_FYROM_90_CI_AS not null,
   LastName             varchar(20)          collate Macedonian_FYROM_90_CI_AS not null,
   Email                varchar(50)          collate Macedonian_FYROM_90_CI_AS null
)
on "PRIMARY"
go

alter table dbo.Employee
   add constraint PK__Employee__7AD04F1160FEF479 primary key (EmployeeId)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: "Order"                                               */
/*==============================================================*/
create table "Order" (
   OrderId              int                  identity,
   CustomerId           int                  null,
   EmployeeId           int                  null,
   OrderStatusId        int                  null,
   OrderNumber          varchar(100)         null,
   Comment              nvarchar(100)        null,
   OrderDate            datetime             not null
)
go

alter table "Order"
   add constraint PK_ORDER primary key (OrderId)
go

/*==============================================================*/
/* Table: OrderDetails                                          */
/*==============================================================*/
create table OrderDetails (
   OrderDetailsId       int                  identity,
   OrderId              int                  not null,
   ProductId            int                  not null,
   Quantity             int                  not null,
   UnitPrice            decimal(8,2)         not null,
   Discount             decimal(8,2)         null
)
go

alter table OrderDetails
   add constraint PK_ORDERDETAILS primary key (OrderDetailsId)
go

/*==============================================================*/
/* Table: OrderStatus                                           */
/*==============================================================*/
create table OrderStatus (
   OrderStatusId        int                  not null,
   StatusDescription    nvarchar(100)        not null
)
go

alter table OrderStatus
   add constraint PK_ORDERSTATUS primary key (OrderStatusId)
go

/*==============================================================*/
/* Table: Product                                               */
/*==============================================================*/
create table dbo.Product (
   ProductId            int                  identity,
   CategoryId           int                  null,
   SuplierId            int                  null,
   ProductName          nvarchar(22)         not null,
   QuantityPerUnit      int                  null,
   UnitPrice            decimal(8,2)         not null,
   UnitsInStock         decimal(8,2)         null,
   ReorderLevel         decimal(8,2)         null,
   IsDomestic           bit                  not null,
   Discontinued         bit                  null,
   BarCode1             varchar(13)          null,
   BarCode2             varchar(13)          null,
   BarCode3             varchar(13)          null,
   BarCode4             varchar(13)          null,
   Created              datetime             null default getdate(),
   Updated              datetime             null default getdate()
)
on "PRIMARY"
go

alter table dbo.Product
   add constraint PK_PRODUCT primary key (ProductId)
go

/*==============================================================*/
/* Table: Promet                                                */
/*==============================================================*/
create table dbo.Promet (
   PrometId             int                  identity(1, 1),
   Created              datetime             not null,
   Quantity             int                  not null,
   Comment              nvarchar(500)        collate Macedonian_FYROM_90_CI_AS null,
   Price                money                not null
)
on "PRIMARY"
go

alter table dbo.Promet
   add constraint PK_Promet primary key (PrometId)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Suplier                                               */
/*==============================================================*/
create table Suplier (
   SuplierId            int                  identity,
   SuplierName          nvarchar(1000)       not null,
   Created              datetime             not null,
   Updated              datetime             not null
)
go

alter table Suplier
   add constraint PK_SUPLIER primary key (SuplierId)
go

alter table dbo.Employee
   add constraint FK_Employee_Department foreign key (DepartmentId)
      references dbo.Department (DepartmentId)
go

alter table "Order"
   add constraint FK_ORDER_REFERENCE_CUSTOMER foreign key (CustomerId)
      references dbo.Customer (CustomerId)
go

alter table "Order"
   add constraint FK_ORDER_REFERENCE_EMPLOYEE foreign key (EmployeeId)
      references dbo.Employee (EmployeeId)
go

alter table "Order"
   add constraint FK_ORDER_REFERENCE_ORDERSTA foreign key (OrderStatusId)
      references OrderStatus (OrderStatusId)
go

alter table OrderDetails
   add constraint FK_ORDERDET_REFERENCE_PRODUCT foreign key (ProductId)
      references dbo.Product (ProductId)
go

alter table OrderDetails
   add constraint FK_ORDERDET_REFERENCE_ORDER foreign key (OrderId)
      references "Order" (OrderId)
go

alter table dbo.Product
   add constraint FK_PRODUCT_REFERENCE_CATEGORY foreign key (CategoryId)
      references Category (CategoryId)
go

alter table dbo.Product
   add constraint FK_PRODUCT_REFERENCE_SUPLIER foreign key (SuplierId)
      references Suplier (SuplierId)
go

