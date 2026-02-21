/* ------------------------------------------------------------------ Wie viele Personendatensätze sind vorhanden?*/
SELECT count(*) FROM Person;


/* ------------------------------------------------------------------ Wie viele Personen wohnen in Dresden?*/
SELECT COUNT(*) AS "People in DD"
FROM Person p, Address a, PersonAddressRelation par
WHERE par.PersonID = p.PersonID AND par.AddressID = a.AddressID AND a.Town = 'Dresden';


/* ------------------------------------------------------------------ Wie viele Personen haben mehr als eine Telefonnummer?*/
SELECT COUNT(PhoneCount) AS "Number of people with more than one telefone number"
FROM (
	SELECT COUNT(ppr.PhoneID) AS PhoneCount
	FROM Person p, PersonPhoneRelation ppr
	WHERE ppr.PersonID=p.PersonID
	GROUP BY ppr.PersonID
) AS PhoneNumbers
WHERE PhoneCount > 1;


/* ------------------------------------------------------------------ Anzahl der Personen pro Ort*/
SELECT DISTINCT a.Town, COUNT(par.AddressID) AS "Anzahl der Personen"
FROM Address a, PersonAddressRelation par
WHERE a.AddressID = par.AddressID
GROUP BY a.Town;


/* ------------------------------------------------------------------ Löschen von Telefonnummern*/
ALTER TABLE PersonPhoneRelation DROP CONSTRAINT PersonPhonePhone;
ALTER TABLE PersonPhoneRelation ADD CONSTRAINT PersonPhonePhone FOREIGN KEY(PhoneID) REFERENCES Phone(PhoneID) ON DELETE CASCADE;
DELETE FROM Phone WHERE (CountryPrefix NOT LIKE '0049') AND (CountryPrefix NOT LIKE '+%');
ALTER TABLE PersonPhoneRelation DROP CONSTRAINT PersonPhonePhone;
ALTER TABLE PersonPhoneRelation ADD CONSTRAINT PersonPhonePhone FOREIGN KEY(PhoneID) REFERENCES Phone(PhoneID);


/* ------------------------------------------------------------------ Upper-Case Name*/
ALTER TABLE Person ADD COLUMN NameUpperCase VARCHAR(255) AFTER FirstName;
UPDATE Person SET NameUpperCase = UPPER(CONCAT(LastName, ' ', FirstName));
