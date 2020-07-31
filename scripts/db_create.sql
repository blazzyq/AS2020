create table Roles
(
    Id          int identity
        constraint PK_Roles
            primary key,
    Name        nvarchar(max) not null,
    DisplayName nvarchar(max) not null
)
go

create table SysFunctions
(
    Id          int identity
        constraint PK_SysFunctions
            primary key,
    Name        nvarchar(max) not null,
    DisplayName nvarchar(max) not null
)
go

create table RoleFunctions
(
    RoleId    int not null
        constraint FK_RoleFunctions_Roles
            references Roles
            on update cascade on delete cascade,
    SysFuncId int not null
        constraint FK_RoleFunctions_SysFunctions
            references SysFunctions
            on update cascade on delete cascade,
    constraint IX_RoleFunctions_Unique
        unique (RoleId, SysFuncId)
)
go

create table Users
(
    Id           int identity
        constraint PK_Users
            primary key,
    Username     nvarchar(max) not null,
    PasswordHash nvarchar(max) not null
)
go

create table UserPics
(
    Id      int identity
        constraint PK_UserPics
            primary key,
    UserId  int            not null
        constraint FK_UserPics_Users
            references Users
            on update cascade on delete cascade,
    Picture varbinary(max) not null
)
go

create table UserProfiles
(
    Id         int identity
        constraint PK_UserProfiles
            primary key,
    UserId     int           not null
        constraint FK_UserProfiles_Users
            references Users
            on update cascade on delete cascade,
    LastName   nvarchar(max) not null,
    FirstName  nvarchar(max) not null,
    MiddleName nvarchar(max) not null,
    BirthDate  datetime      not null,
    Sex        smallint      not null
        constraint CK_Profiles_Sex
            check ([Sex] = 0 OR [Sex] = 1),
    Email      nvarchar(max) not null,
    Phone      nvarchar(max) not null
)
go

create table UserRoles
(
    UserId int not null
        constraint FK_UserRoles_Users
            references Users
            on update cascade on delete cascade,
    RoleId int not null
        constraint FK_UserRoles_Roles
            references Roles
            on update cascade on delete cascade,
    constraint IX_UserRoles_Unique
        unique (UserId, RoleId)
)
go

create table VehicleAttributes
(
    Id          int identity
        constraint PK_VehicleAttributes
            primary key,
    Name        nvarchar(max) not null,
    DisplayName nvarchar(max) not null,
    Type        smallint      not null
        constraint CK_VehicleAttributes_Type
            check ([Type] = 3 OR [Type] = 2 OR [Type] = 1 OR [Type] = 0)
)
go

create table VehicleCategories
(
    Id          int identity
        constraint PK_VehicleCategories
            primary key,
    Name        nvarchar(max) not null,
    DisplayName nvarchar(max) not null
)
go

create table VehicleManufacturers
(
    Id          int identity
        constraint PK_VehicleManufacturers
            primary key,
    Name        nvarchar(max) not null,
    DisplayName nvarchar(max) not null
)
go

create table Vehicles
(
    Id             int identity
        constraint PK_Vehicles
            primary key,
    ManufacturerId int           not null
        constraint FK_Vehicles_VehicleManufacturers
            references VehicleManufacturers
            on update cascade on delete cascade,
    ModelName      nvarchar(max) not null,
    ProductionDate datetime      not null,
    RegNumber      nvarchar(max) not null,
    StartUsageDate datetime      not null,
    EndUsageDate   datetime,
    CategoryId     int           not null
        constraint FK_Vehicles_VehicleCategories
            references VehicleCategories
            on update cascade on delete cascade
)
go

create table Orders
(
    Id                int identity
        constraint PK_Orders
            primary key,
    UserId            int                     not null
        constraint FK_Orders_Users
            references Users,
    CreationDateTime  datetime                not null,
    StartDateTime     datetime,
    EndDateTime       datetime,
    StartPointAddress nvarchar(max)           not null,
    EndPointAddress   nvarchar(max)           not null,
    VehicleId         int
        constraint FK_Orders_Vehicles
            references Vehicles
            on update cascade on delete cascade,
    OperatorId        int                     not null
        constraint FK_Orders_Users_Operator
            references Users,
    Status            smallint
        constraint DF_Orders_Status default 0 not null
        constraint CK_Orders_Status
            check ([Status] = 4 OR [Status] = 3 OR [Status] = 2 OR [Status] = 1 OR [Status] = 0)
)
go

create table VehicleAttributeValues
(
    Id            int identity
        constraint PK_VehicleAttributeValues
            primary key,
    AttributeId   int not null
        constraint FK_VehicleAttributeValues_VehicleAttributes
            references VehicleAttributes
            on update cascade on delete cascade,
    VehicleId     int not null
        constraint FK_VehicleAttributeValues_Vehicles
            references Vehicles
            on update cascade on delete cascade,
    IntValue      int,
    FloatValue    float,
    StringValue   nvarchar(max),
    DateTimeValue datetime
)
go

create table VehicleCycles
(
    Id                       int identity
        constraint PK_VehicleCycles
            primary key,
    VehicleId                int                                       not null
        constraint FK_VehicleCycles_Vehicles
            references Vehicles
            on update cascade on delete cascade,
    InspIntervalTime         float,
    CurrInspFlyTime          float
        constraint DF_VehicleCycles_CurrentFlyTime default 0           not null,
    InspIntervalDist         float,
    CurrInspFlyDist          float
        constraint DF_VehicleCycles_CurrentFlyDistance default 0       not null,
    InspIntervalRegType      smallint
        constraint DF_VehicleCycles_InspIntervalRegType default 0      not null
        constraint CK_VehicleCycles_InspIntervalRegType
            check ([InspIntervalRegType] = 3 OR [InspIntervalRegType] = 2 OR [InspIntervalRegType] = 1 OR
                   [InspIntervalRegType] = 0),
    InspIntervalRegTimeOfDay time
        constraint DF_VehicleCycles_InspIntervalRegTimeOfDay default '0:00:00',
    InspIntervalRegLastDate  datetime
        constraint DF_VehicleCycles_InspIntervalRegStartDate default getdate(),
    RepIntervalTime          float,
    CurrRepFlyTime           float
        constraint DF_VehicleCycles_CurrentRepairFlyTime default 0     not null,
    RepIntervalDist          float,
    CurrRepFlyDist           float
        constraint DF_VehicleCycles_CurrentRepairFlyDistance default 0 not null
)
go

create table VehicleInspections
(
    Id             int identity
        constraint PK_VehicleInspections
            primary key,
    VehicleId      int      not null
        constraint FK_VehicleInspections_Vehicles
            references Vehicles
            on update cascade on delete cascade,
    StartDate      datetime not null,
    EndDate        datetime,
    WorkerId       int      not null
        constraint FK_VehicleInspections_Users
            references Users,
    DocumentNumber nvarchar(max)
)
go

create table VehiclePics
(
    Id        int identity
        constraint PK_VehiclePics
            primary key,
    VehicleId int            not null
        constraint FK_VehiclePics_Vehicles
            references Vehicles
            on update cascade on delete cascade,
    Picture   varbinary(max) not null
)
go

create table VehicleRepairs
(
    Id             int identity
        constraint PK_VehicleRepairs
            primary key,
    VehicleId      int      not null
        constraint FK_VehicleRepairs_Vehicles
            references Vehicles
            on update cascade on delete cascade,
    Type           smallint not null
        constraint CK_VehicleRepairs_Type
            check ([Type] = 2 OR [Type] = 1 OR [Type] = 0),
    StartDate      datetime not null,
    EndDate        datetime,
    WorkerId       int      not null
        constraint FK_VehicleRepairs_Users
            references Users,
    DocumentNumber nvarchar(max)
)
go

create table sysdiagrams
(
    name         sysname not null,
    principal_id int     not null,
    diagram_id   int identity
        primary key,
    version      int,
    definition   varbinary(max),
    constraint UK_principal_name
        unique (principal_id, name)
)
go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'TABLE', 'sysdiagrams'
go


