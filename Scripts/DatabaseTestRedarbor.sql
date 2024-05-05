CREATE DATABASE TestRedarbor;

USE TestRedarbor;

Create Table Employees(
	EmployeeId int Identity(1,1) Primary key,
	CompanyId int not null,
	CreatedOn datetime,
	DeletedOn datetime,
	Email varchar(100) not null,
	Fax varchar(20),
	[Name] varchar(50),
	Lastlogin datetime,
	[Password] varchar(255) not null,
	PortalId int not null,
	RoleId int not null,
	StatusId int not null,
	Telephone varchar(20),
	UpdatedOn datetime,
	Username varchar(20) not null,
	IsDeleted bit not null default 0
);

Create table Users(
	UserId int identity(1,1) Primary key,
	Username varchar(20) not null,
	[Password] varchar(255) not null,
	IsActive bit not null default 1,
	CreatedOn datetime default GETDATE()
);

INSERT INTO Users (Username, [Password]) VALUES ('javierski', 'amF2aWVyc2tp');