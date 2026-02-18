
using System.Runtime.InteropServices.Marshalling;
using Microsoft.AspNetCore.Http.HttpResults;
using PeopleWS.DB.Models;
using PeopleWS.DB;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/person/{id}", Results<Ok<PersonWS>, NotFound> (int id) =>
{
	Console.WriteLine("GET-call to /person/" + id.ToString());
	//PersonWS personWS = new PersonWS("1", "Stefan", "Lebelt", new DateOnly(year: 1981, month: 1, day: 19), [new PostalAddressWS("01728", "Bannewitz", "Südhang", "20")], [new PhoneWS(49, 172, 6004535, "Mobile"), new PhoneWS(49, 175, 6615393, "Mobile Work")]);

	SchleupenHomeworkContext context = new SchleupenHomeworkContext();
	Person person = context.People.Where(p => p.PersonId == id).FirstOrDefault();
	if (person == null)
	{
		return TypedResults.NotFound();
	}
	else
	{
		return TypedResults.Ok(new PersonWS(person.PersonId, person.FirstName, person.LastName, DateOnly.FromDateTime((DateTime)person.DateOfBirth), [], []));
	}
});
app.MapGet("/search/{term}", Results<Ok<List<PersonWS>>, NotFound> (string term) =>
{
	Console.WriteLine("GET-call to /search/" + term.ToString());
	List<PersonWS> Result = [];
	Result.Add(new PersonWS(1, "Stefanus", "Lebelt", new DateOnly(year: 1981, month: 1, day: 19), [new PostalAddressWS("01728", "Bannewitz", "Südhang", "20"), new PostalAddressWS("02708", "Großschweidnitz", "Fliederweg", "19")], [new PhoneWS(49, 172, 6004535, "Mobile"), new PhoneWS(49, 175, 6615393, "Mobile Work")]));

	/*
	SchleupenHomeworkContext context = new SchleupenHomeworkContext();
	foreach( Person p in context.People.Where(p => p.PersonId == id) )
	{
		Console.WriteLine(p.LastName);
	}
	*/

	return Result.Count == 0 ? TypedResults.NotFound() : TypedResults.Ok(Result);
});
app.MapGet("/all", Results<Ok<List<PersonWS>>, NotFound> () =>
{
	Console.WriteLine("GET-call to /all");
	List<PersonWS> Result = [];
	Result.Add(new PersonWS(1, "Stefan", "Lebelt", new DateOnly(year: 1981, month: 1, day: 19), [new PostalAddressWS("01728", "Bannewitz", "Südhang", "20"), new PostalAddressWS("02708", "Großschweidnitz", "Fliederweg", "19")], [new PhoneWS(49, 172, 6004535, "Mobile"), new PhoneWS(49, 175, 6615393, "Mobile Work")]));

	/*
	SchleupenHomeworkContext context = new SchleupenHomeworkContext();
	foreach( Person p in context.People.Where(p => p.PersonId == id) )
	{
		Console.WriteLine(p.LastName);
	}
	*/

	return Result.Count == 0 ? TypedResults.NotFound() : TypedResults.Ok(Result);
});
app.MapPost("person/", (PersonWS person) =>
{
	Console.WriteLine("POST-call to /person/" + person.ToString());
	//TODO implement creation or update
	return TypedResults.Created("/person/{id}, person");
});

app.Run();

public record PhoneWS(int CountryPrefixWS, int RegionPrefixWS, int NumberWS, string PhoneTypeWS);
public record PostalAddressWS(string PostalCodeWS, string TownWS, string StreetWS, string StreetNumberWS);
public record PersonWS(int ID, string FirstNameWS, string LastNameWS, DateOnly DateOfBirthWS, List<PostalAddressWS> PostalAddressesWS, List<PhoneWS> PhoneNumbersWS);