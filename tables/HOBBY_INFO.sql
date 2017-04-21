create table HOBBY_INFO (
	alias varchar (10),
	hobby varchar (100),
	supplier varchar (10),
	created datetime default GETDATE(),
	modified datetime default GETDATE(),
	PRIMARY KEY(alias, hobby, supplier)
)
