
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
app.MapGet("/search/{term}/{sortBy?}/{ascDesc?}", Results<Ok<List<PersonWS>>, NotFound> (string term, string? sortBy, string? ascDesc) =>
{
	Console.WriteLine("GET-call to /search/" + term.ToString() + "/" + sortBy + "/" + ascDesc);
	List<PersonWS> Result = [];
	IQueryable<Person> query = new SchleupenHomeworkContext().People.Include(person => person.Addresses).Include(person => person.Phones);
	if( !"◕‿◕".Equals(term))
	{
		query = query.Where(p => p.FirstName == term || p.LastName == term);
	}
	if ("first".Equals(sortBy))
	{
		if ("desc".Equals(ascDesc))
		{
			query = query.OrderByDescending(p => p.FirstName);
		}
		else
		{
			query = query.OrderBy(p => p.FirstName);
		}
	}
	else if ("last".Equals(sortBy))
	{
		if ("desc".Equals(ascDesc))
		{
			query = query.OrderByDescending(p => p.LastName);
		}
		else
		{
			query = query.OrderBy(p => p.LastName);
		}
	}
	else if ("birth".Equals(sortBy))
	{
		if ("desc".Equals(ascDesc))
		{
			query = query.OrderByDescending(p => p.DateOfBirth);
		}
		else
		{
			query = query.OrderBy(p => p.DateOfBirth);
		}
	}
	foreach (Person person in query.ToList())
	{
		Result.Add(PersonWS.FromDbModel(person));
	}
	return Result.Count == 0 ? TypedResults.NotFound() : TypedResults.Ok(Result);
});
app.MapPost("person/", (PersonWS person) =>
{
	Console.WriteLine("POST-call to /person/" + person.ToString());
	SchleupenHomeworkContext context = new();
	Person? existingPerson = context.People.Include(person => person.Addresses).Include(person => person.Phones).Where(p => p.PersonId == person.ID).FirstOrDefault();
	if (existingPerson == null)
	{
		context.Add(person.ToDbModel);
	}
	else
	{
		person.UpdateDbModel(existingPerson);
	}

	context.SaveChanges();
	return TypedResults.Created("/person/{id}, person");
});

app.Run();