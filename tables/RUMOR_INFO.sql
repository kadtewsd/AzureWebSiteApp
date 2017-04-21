create table RUMOR_INFO (
	alias varchar (10),
	rumor varchar (100),
	supplier varchar (10),
	created datetime default GETDATE(),
	modified datetime default GETDATE(),
	PRIMARY KEY(alias, rumor, supplier)
)
