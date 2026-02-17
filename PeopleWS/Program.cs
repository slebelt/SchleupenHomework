
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/person/{id}", Results<Ok<Person>, NotFound> (int id) =>
{
	Console.WriteLine("GET-call to /person/" + id.ToString());
	Person person = new Person("1", "Stefan", "Lebelt", new DateOnly(year: 1981, month: 1, day: 19), [new PostalAddress("01728", "Bannewitz", "Südhang", "20")], []);
	//TODO implement search
	return person is null ? TypedResults.NotFound() : TypedResults.Ok(person);
});
app.MapGet("/search/{term}", Results<Ok<List<Person>>, NotFound> (string term) =>
{
	Console.WriteLine("GET-call to /search/" + term.ToString());
	List<Person> Result = [];
	Result.Add(new Person("1", "Stefanus", "Lebelt", new DateOnly(year: 1981, month: 1, day: 19), [new PostalAddress("01728", "Bannewitz", "Südhang", "20"), new PostalAddress("02708", "Großschweidnitz", "Fliederweg", "19")], [new Phone(49, 172, 6004535, "Mobile"), new Phone(49, 175, 6615393, "Mobile Work")]));
	//TODO implement search
	return Result.Count == 0 ? TypedResults.NotFound() : TypedResults.Ok(Result);
});
app.MapGet("/all", Results<Ok<List<Person>>, NotFound> () =>
{
	Console.WriteLine("GET-call to /all");
	List<Person> Result = [];
	Result.Add(new Person("1", "Stefan", "Lebelt", new DateOnly(year: 1981, month: 1, day: 19), [new PostalAddress("01728", "Bannewitz", "Südhang", "20"), new PostalAddress("02708", "Großschweidnitz", "Fliederweg", "19")], [new Phone(49, 172, 6004535, "Mobile"), new Phone(49, 175, 6615393, "Mobile Work")]));
	//TODO implement search
	return Result.Count == 0 ? TypedResults.NotFound() : TypedResults.Ok(Result);
});
app.MapPost("person/", (Person person) =>
{
	Console.WriteLine("POST-call to /person/");
	//TODO implement creation or update
	return TypedResults.Created("/person/{id}, person");
});

app.Run();

public record Phone(int CountryPrefix, int RegionPrefix, int Number, string PhoneType);
public record PostalAddress(string PostalCode, string Town, string Street, string StreetNumber);
public record Person(string ID, string FirstName, string LastName, DateOnly DateOfBirth, List<PostalAddress> PostalAddresses, List<Phone> PhoneNumbers);