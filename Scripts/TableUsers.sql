Create table Users(
	UserId int identity(1,1) Primary key,
	Username varchar(20) not null,
	[Password] varchar(255) not null,
	IsActive bit not null default 1,
	CreatedOn datetime default GETDATE()
);

INSERT INTO Users (Username, [Password]) VALUES ('javierski', 'amF2aWVyc2tp');