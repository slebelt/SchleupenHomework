
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/person/{id}", Results<Ok<Person>, NotFound> (int id) =>
{
	Person person = new Person(1, "Stefan", "Lebelt", DateTime.Parse("1981-01-19"), [], []);
	//TODO implement search
	return person is null ? TypedResults.NotFound() : TypedResults.Ok(person);
});
app.MapGet("/person/search/{term}", Results<Ok<List<Person>>, NotFound> (string term) =>
{
	List<Person> Result = [];
	Result.Add(new Person(1, "Stefan", "Lebelt", DateTime.Parse("1981-01-19"), [], []));
	//TODO implement search
	return Result.Count == 0 ? TypedResults.NotFound() : TypedResults.Ok(Result);
});
app.MapPost("person/", (Person person) =>
{
	//TODO implement creation or update
	return TypedResults.Created("/person/{id}, person");
});

app.Run();

public record Phone(int CountryPrefix, int RegionPrefix, int Number, string phoneType);
public record PostalAddress(string PostalCode, string Town, string Street, string StreetNumber);
public record Person(int ID, string FirstName, string LastName, DateTime DataOfBirth, List<PostalAddress> PostalAddresses, List<Phone> PhoneNumbers);