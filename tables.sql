drop table if exists `User`;
create table `User` (
	`ID` integer not null primary key autoincrement,
	`Name` text not null,
	`PwHash` varchar(100) not null unique,
	`PwSalt` varchar(100) not null unique,
	`DoB` integer not null
);

drop table if exists `Auction`;
create table `Auction` (
	`ID` integer not null primary key autoincrement,
	`Name` text not null,
	`Seller` text not null,
	`Startbid` integer not null,
	`AuctionEnd` text not null,
	`Image` varchar(100) not null unique,
	`Description` varchar(100) not null unique
);

drop table if exists `Bids`;
create table `Bids` (
	`BidId` integer not null,
	`BidPrice` integer not null,
	`BidderId` integer
);	


