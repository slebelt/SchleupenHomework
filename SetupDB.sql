DROP DATABASE SchleupenHomework;
CREATE DATABASE SchleupenHomework CHARACTER SET utf8;

USE SchleupenHomework;

/* ------------------------------------------------------------------ Tables*/
CREATE TABLE Person (
	PersonID INT NOT NULL AUTO_INCREMENT,
	LastName VARCHAR(255),
	FirstName VARCHAR(255),
	DateOfBirth DATE,
	PRIMARY KEY(PersonID)
);

CREATE TABLE Address (
	AddressID INT NOT NULL AUTO_INCREMENT,
	Street VARCHAR(255),
	StreetNumber VARCHAR(255),
	PostalCode INT,
	Town VARCHAR(255),
	PRIMARY KEY(AddressID)
);

CREATE TABLE Phone (
	PhoneID INT NOT NULL AUTO_INCREMENT,
	PhoneType VARCHAR(7),
	CountryPrefix VARCHAR(4),
	TownPrefix INT,
	Number INT,
	PRIMARY KEY(PhoneID)
);

CREATE TABLE PersonAddressRelation (
	PersonID INT NOT NULL,
	AddressID INT NOT NULL,
	CONSTRAINT PersonAddressPerson FOREIGN KEY(PersonID) REFERENCES Person(PersonID) /*ON DELETE CASCADE - would be done like this in the real world, but is explicitely not wanted by the spec*/,
	CONSTRAINT PersonAddressAddress FOREIGN KEY(AddressID) REFERENCES Address(AddressID) /*ON DELETE CASCADE*/,
	CONSTRAINT PersonAddressUnique UNIQUE(PersonID, AddressID)
);

CREATE TABLE PersonPhoneRelation (
	PersonID INT NOT NULL,
	PhoneID INT NOT NULL,
	CONSTRAINT PersonPhonePerson FOREIGN KEY(PersonID) REFERENCES Person(PersonID) /*ON DELETE CASCADE*/,
	CONSTRAINT PersonPhonePhone FOREIGN KEY(PhoneID) REFERENCES Phone(PhoneID) /*ON DELETE CASCADE*/,
	CONSTRAINT PersonPhoneUnique UNIQUE(PersonID, PhoneID)
);

/* ------------------------------------------------------------------ View*/
CREATE VIEW View AS
	SELECT
		p.FirstName,
		p.LastName,
		COALESCE(addr.Addresses, '-') as Addresses,
		COALESCE(tel.Numbers, '-') as PhoneNumbers
	FROM Person p
	LEFT JOIN (
		SELECT
			par.PersonID,
			GROUP_CONCAT(
				CONCAT(a.Street, a.StreetNumber, ', ', a.PostalCode, ' ', a.Town)
				SEPARATOR ' | '
			) AS Addresses
		FROM PersonAddressRelation par
		JOIN Address a
			ON a.AddressID = par.AddressID
		GROUP BY par.PersonID
	) addr
		ON addr.PersonID = p.PersonID
	LEFT JOIN (
		SELECT
			ppr.PersonID,
			GROUP_CONCAT(
				CONCAT(t.CountryPrefix, ' ', t.TownPrefix, ' ', t.Number)
				SEPARATOR ' | '
			) AS Numbers
		FROM PersonPhoneRelation ppr
		JOIN Phone t
			ON t.PhoneID = ppr.PhoneID
		GROUP BY ppr.PersonID
	) tel
		ON tel.PersonID = p.PersonID
	ORDER BY p.PersonID;
	
/* ------------------------------------------------------------------ Sample Data*/
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Lebelt', 'Stefan', '1981-01-19');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Roddenberry', 'Gene', '1921-08-19');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Berman', 'Rick', '1945-12-25');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Piller', 'Michael', '1948-05-30');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Braga', 'Brannon', '1965-08-14');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Steward', 'Patrick', '1940-07-13');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Frakes', 'Jonathan', '1952-08-19');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Spiner', 'Brent', '1949-02-02');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Burton', 'Levar', '1957-02-16');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Sirtis', 'Marina', '1955-03-29');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Dorn', 'Michael', '1952-12-09');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('McFadden', 'Gates', '1949-03-02');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Barrett', 'Majel', '1932-02-23');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Wheeton', 'Wil', '1972-07-29');
INSERT INTO Person (LastName, FirstName, DateOfBirth) values('Meaney', 'Colm', '1953-05-30');

INSERT INTO Address (Street, StreetNumber, PostalCode, Town) values ('Südhang', '20', '1728', 'Possendorf');
INSERT INTO Address (Street, StreetNumber, PostalCode, Town) values ('Buchenstraße', '16 B', '1097', 'Dresden');
INSERT INTO Address (Street, StreetNumber, PostalCode, Town) values ('Hauptstraße', '20', '1097', 'Dresden');
INSERT INTO Address (Street, StreetNumber, PostalCode, Town) values ('Rudolf Leonard Straße', '23', '1097', 'Dresden');
INSERT INTO Address (Street, StreetNumber, PostalCode, Town) values ('Cottaer Straße', '4', '1159', 'Dresden');
INSERT INTO Address (Street, StreetNumber, PostalCode, Town) values ('Prager Straße', '1', '1069', 'Dresden');

INSERT INTO Phone (PhoneType, CountryPrefix, TownPrefix, Number) values ('Home', '+49', '35206', '261648');
INSERT INTO Phone (PhoneType, CountryPrefix, TownPrefix, Number) values ('Mobile', '+49', '172', '6004535');
INSERT INTO Phone (PhoneType, CountryPrefix, TownPrefix, Number) values ('Mobile', '0049', '175', '6615393');
INSERT INTO Phone (PhoneType, CountryPrefix, TownPrefix, Number) values ('2del', '49', '35206', '261648');

INSERT INTO PersonAddressRelation (PersonID, AddressID) values ('1', '1'); /*Stefan Lebelt*/
INSERT INTO PersonAddressRelation (PersonID, AddressID) values ('1', '2'); /*Stefan Lebelt*/
INSERT INTO PersonAddressRelation (PersonID, AddressID) values ('2', '3'); /*Gene Roddenberry*/
INSERT INTO PersonAddressRelation (PersonID, AddressID) values ('3', '4'); /*Rick Berman*/
INSERT INTO PersonAddressRelation (PersonID, AddressID) values ('4', '5'); /*Michael Piller*/
INSERT INTO PersonAddressRelation (PersonID, AddressID) values ('5', '5'); /*Brannon Braga*/
INSERT INTO PersonAddressRelation (PersonID, AddressID) values ('5', '6'); /*Brannon Braga*/

INSERT INTO PersonPhoneRelation (PersonID, PhoneID) values ('1', '1');
INSERT INTO PersonPhoneRelation (PersonID, PhoneID) values ('1', '2');
INSERT INTO PersonPhoneRelation (PersonID, PhoneID) values ('1', '3');
INSERT INTO PersonPhoneRelation (PersonID, PhoneID) values ('1', '4');
