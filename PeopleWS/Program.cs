
using System.Runtime.InteropServices.Marshalling;
using Microsoft.AspNetCore.Http.HttpResults;
using PeopleWS.DB.Models;
using PeopleWS.DB;
using PeopleWS.WsModels;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/person/{id}", Results<Ok<PersonWS>, NotFound> (int id) =>
{
	Console.WriteLine("GET-call to /person/" + id.ToString());
	//PersonWS personWS = new PersonWS("1", "Stefan", "Lebelt", new DateOnly(year: 1981, month: 1, day: 19), [new PostalAddressWS("01728", "Bannewitz", "Südhang", "20")], [new PhoneWS(49, 172, 6004535, "Mobile"), new PhoneWS(49, 175, 6615393, "Mobile Work")]);

	Person person = new SchleupenHomeworkContext().People.Where(p => p.PersonId == id).FirstOrDefault();
	if (person == null)
	{
		return TypedResults.NotFound();
	}
	else
	{
		return TypedResults.Ok(PersonWS.FromDbModel(person));
	}
});
app.MapGet("/search/{term}", Results<Ok<List<PersonWS>>, NotFound> (string term) =>
{
	Console.WriteLine("GET-call to /search/" + term.ToString());
	List<PersonWS> Result = [];
	foreach( Person person in new SchleupenHomeworkContext().People.Where(p => p.FirstName == term || p.LastName == term) )
	{
		Result.Add(PersonWS.FromDbModel(person));
	}
	return Result.Count == 0 ? TypedResults.NotFound() : TypedResults.Ok(Result);
});
app.MapGet("/all", Results<Ok<List<PersonWS>>, NotFound> () =>
{
	Console.WriteLine("GET-call to /all");
	List<PersonWS> Result = [];
	foreach( Person person in new SchleupenHomeworkContext().People )
	{
		Result.Add(PersonWS.FromDbModel(person));
	}
	return Result.Count == 0 ? TypedResults.NotFound() : TypedResults.Ok(Result);
});
app.MapPost("person/", (PersonWS person) =>
{
	Console.WriteLine("POST-call to /person/" + person.ToString());
	//TODO implement creation or update
	return TypedResults.Created("/person/{id}, person");
});

app.Run();
