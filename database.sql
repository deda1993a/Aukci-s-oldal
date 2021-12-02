create table `User` (
	`ID` integer not null primary key,
	`Name` text,
	`PwHash` varchar(100),
	`DoB` integer not null
);