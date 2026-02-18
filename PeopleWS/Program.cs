
using System.Runtime.InteropServices.Marshalling;
using Microsoft.AspNetCore.Http.HttpResults;
using PeopleWS.DB.Models;
using PeopleWS.DB;
using PeopleWS.WsModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/person/{id}", async Task<Results<Ok<PersonWS>, NotFound>> (int id) =>
{
	Console.WriteLine("GET-call to /person/" + id.ToString());
	Person? person = new SchleupenHomeworkContext().People.Include(person => person.Addresses).Include(person => person.Phones).Where(p => p.PersonId == id).FirstOrDefault();
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
	foreach (Person person in new SchleupenHomeworkContext().People.Include(person => person.Addresses).Include(person => person.Phones).Where(p => p.FirstName == term || p.LastName == term))
	{
		Result.Add(PersonWS.FromDbModel(person));
	}
	return Result.Count == 0 ? TypedResults.NotFound() : TypedResults.Ok(Result);
});
app.MapGet("/all", Results<Ok<List<PersonWS>>, NotFound> () =>
{
	Console.WriteLine("GET-call to /all");
	List<PersonWS> Result = [];
	foreach (Person person in new SchleupenHomeworkContext().People.Include(person => person.Addresses).Include(person => person.Phones))
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