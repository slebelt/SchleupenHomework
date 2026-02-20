/* ------------------------------------------------------------------ Wie viele Personendatensätze sind vorhanden?*/
SELECT count(*) FROM Person;


/* ------------------------------------------------------------------ Wie viele Personen wohnen in Dresden?*/
SELECT COUNT(*)
FROM Person p, Address a, PersonAddressRelation par
WHERE par.PersonID = p.PersonID AND par.AddressID = a.AddressID AND a.Town = 'Dresden';


/* ------------------------------------------------------------------ Wie viele Personen haben mehr als eine Telefonnummer?*/
SELECT COUNT(AddressCount)
FROM (
	SELECT COUNT(prv.AddressID) AS AddressCount
	FROM Person p, PersonAddressRelation prv
	WHERE prv.PersonID=p.PersonID
	GROUP BY prv.PersonID) AS AddressNumbers 
WHERE AddressCount > 1;


/* ------------------------------------------------------------------ Anzahl der Personen pro Ort*/
SELECT DISTINCT a.Town, COUNT(par.AddressID)
FROM Address a, PersonAddressRelation par
WHERE a.AddressID = par.AddressID
GROUP BY a.Town;


/* ------------------------------------------------------------------ Löschen von Telefonnummern*/
DELETE p, ppr
FROM Phone p, PersonPhoneRelation ppr
WHERE (p.PhoneID=ppr.PhoneID) 
	AND (
		(p.CountryPrefix NOT LIKE '0049')
		AND (p.CountryPrefix NOT LIKE'+%')
	);


/* ------------------------------------------------------------------ Upper-Case Name*/
ALTER TABLE Person ADD COLUMN NameUpperCase VARCHAR(255) AFTER FirstName;
UPDATE Person SET NameUpperCase = UPPER(CONCAT(LastName, ' ', FirstName));
