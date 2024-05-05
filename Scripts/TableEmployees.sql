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