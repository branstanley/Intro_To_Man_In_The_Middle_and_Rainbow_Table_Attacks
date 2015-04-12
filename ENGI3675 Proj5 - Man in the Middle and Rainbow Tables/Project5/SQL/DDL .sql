drop database if exists "Project5";

drop role if exists "Antoine";

create role "Antoine" login;
comment on role "Antoine" is 'This is the user';

drop database if exists "Project5";

create database "Project5";
comment on database "Project5" is 'This is the Project5 database';

grant connect on database "Project5" to "Antoine";