using Microsoft.AspNetCore.Hosting.Server.Features;
using PeopleWS.DB.Models;

namespace PeopleWS.WsModels;

public record PersonWS(int ID, string? FirstNameWS, string? LastNameWS, DateOnly? DateOfBirthWS, List<PostalAddressWS> PostalAddressesWS, List<PhoneWS> PhoneNumbersWS)
{
	public Person ToDbModel()
	{
		Person personDB = new()
		{
			PersonId = ID,
			Addresses = [],
			Phones = []
		};
		UpdateDbModel(personDB);
		return personDB;
	}
	private void UpdateDbAddresses(Person existingPerson)
	{
		List<Address> allExistingDbAddresses = [];
		allExistingDbAddresses.AddRange(existingPerson.Addresses);
		List<PostalAddressWS> allWsAddresses = [];
		allWsAddresses.AddRange(PostalAddressesWS);
		foreach (PostalAddressWS addressWS in PostalAddressesWS)
		{
			foreach (Address addressDB in existingPerson.Addresses)
			{
				if (addressWS.ID == addressDB.AddressId)
				{
					addressWS.UpdateDbModel(addressDB);
					allExistingDbAddresses.Remove(addressDB);
					allWsAddresses.Remove(addressWS);
					break;
				}
			}
		}
		foreach (Address removedDbAddress in allExistingDbAddresses)
		{
			existingPerson.Addresses.Remove(removedDbAddress);
		}
		foreach (PostalAddressWS newWsAddress in allWsAddresses)
		{
			existingPerson.Addresses.Add(newWsAddress.ToDbModel());
		}
	}
	private void UpdateDbPhones(Person existingPerson)
	{
		List<Phone> allExistingDbPhones = [];
		allExistingDbPhones.AddRange(existingPerson.Phones);
		List<PhoneWS> allWsPhones = [];
		allWsPhones.AddRange(PhoneNumbersWS);
		foreach (PhoneWS phoneWS in PhoneNumbersWS)
		{
			foreach (Phone phoneDB in existingPerson.Phones)
			{
				if (phoneWS.ID == phoneDB.PhoneId)
				{
					phoneWS.UpdateDbModel(phoneDB);
					allExistingDbPhones.Remove(phoneDB);
					allWsPhones.Remove(phoneWS);
					break;
				}
			}
		}
		foreach (Phone removedDbPhone in allExistingDbPhones)
		{
			existingPerson.Phones.Remove(removedDbPhone);
		}
		foreach (PhoneWS newWsPhone in allWsPhones)
		{
			existingPerson.Phones.Add(newWsPhone.ToDbModel());
		}
	}
	public void UpdateDbModel(Person existingPerson)
	{
		existingPerson.FirstName = FirstNameWS;
		existingPerson.LastName = LastNameWS;
		existingPerson.DateOfBirth = DateOfBirthWS == null ? null : ((DateOnly)DateOfBirthWS).ToDateTime(TimeOnly.Parse("00:00 PM"));
		UpdateDbAddresses(existingPerson);
		UpdateDbPhones(existingPerson);
	}
	public static PersonWS FromDbModel(Person person)
	{
		List<PostalAddressWS> postalAddresses = [];
		foreach (Address address in person.Addresses)
		{
			postalAddresses.Add(PostalAddressWS.FromDbModel(address));
		}
		List<PhoneWS> phones = [];
		foreach (Phone phone in person.Phones)
		{
			phones.Add(PhoneWS.FromDbModel(phone));
		}
#pragma warning disable CS8629 // Nullable value type may be null.
		return new PersonWS(person.PersonId, person.FirstName, person.LastName, person.DateOfBirth == null ? null : DateOnly.FromDateTime((DateTime)person.DateOfBirth), postalAddresses, phones);
#pragma warning restore CS8629 // Nullable value type may be null.
	}
}