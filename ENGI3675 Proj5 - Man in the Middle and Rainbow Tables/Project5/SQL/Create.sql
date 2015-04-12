drop table if exists authen cascade;

CREATE TABLE authen(
	username text primary key,
	pass text not null
);

grant select on table "authen" to "Antoine";

grant all PRIVILEGES on table "authen" to "Antoine";

comment on table authen is 'This is table has user names and hashed password';
comment on column authen.username is 'authen Names';
comment on column authen.pass is 'hashed password';