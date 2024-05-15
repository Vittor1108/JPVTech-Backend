USE JpvTech;

CREATE TABLE Person (
	Id int Primary key IDENTITY(1,1),
	Name varchar(180) not null,
	CPF varchar(11) unique not null,
	IncomeValue decimal(10,2) not null,
	DateBirth Datetime not null,
	CreatedAt Datetime Default(GETDATE()),
	UpdatedAt Datetime
);