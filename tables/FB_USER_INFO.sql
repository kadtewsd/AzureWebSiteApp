create table FB_USER_INFO (
	ID varchar (20) primary key,
	user_name varchar (100),
	birth_day varchar (20),
	created datetime default GETDATE(),
	modified datetime default GETDATE(),
)