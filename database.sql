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
	`BidderId` integer not null
);	

INSERT INTO Auction (Name, Seller, Startbid, AuctionEnd, Image, Description)
VALUES ('ora', 'Cecil', '2', '2021-12-31T12:17', 'tht.bmp','egy jo allapotban levo ora')

SELECT Max(BidPrice) FROM Bids Where BidId='2';

SELECT BidId, MAX (BidPrice), BidderId 
FROM Bids 
WHERE BidPrice NOT IN (SELECT Max (BidPrice) 
FROM Bids) AND BidId=@bd.BidId; 

SELECT BidId, Max(BidPrice), BidderId 
FROM Bids 
WHERE BidId='2';
