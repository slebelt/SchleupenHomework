using Microsoft.AspNetCore.Hosting.Server.Features;
using PeopleWS.DB.Models;

namespace PeopleWS.WsModels;

public record PersonWS(int ID, string? FirstNameWS, string? LastNameWS, DateOnly? DateOfBirthWS, List<PostalAddressWS> PostalAddressesWS, List<PhoneWS> PhoneNumbersWS)
{
	public static PersonWS FromDbModel(Person person)
	{
		List<PostalAddressWS> postalAddresses = [];
		foreach( Address address in person.Addresses )
		{
			postalAddresses.Add(PostalAddressWS.FromDbModel(address));
		}
		List<PhoneWS> phones = [];
		foreach( Phone phone in person.Phones )
		{
			phones.Add(PhoneWS.FromDbModel(phone));
		}
#pragma warning disable CS8629 // Nullable value type may be null.
		return new PersonWS(person.PersonId, person.FirstName, person.LastName, person.DateOfBirth == null ? null : DateOnly.FromDateTime((DateTime)person.DateOfBirth), postalAddresses, phones);
#pragma warning restore CS8629 // Nullable value type may be null.
	}
}