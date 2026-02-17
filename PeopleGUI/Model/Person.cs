using System.Text.Json.Serialization;

namespace PeopleGUI.Model;

public class Person(string? id, string? lastName, string? firstName)
{
	public bool Dirty { get; private set; } = false;
	[JsonPropertyName("id")]
	public string? ID { get; } = id;

	[JsonPropertyName("firstName")]
	public string? FirstNameJSON { get; set; } = firstName;
	public string? FirstName
	{
		get => FirstNameJSON;
		set
		{
			FirstNameJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("lastName")]
	public string? LastNameJSON { get; set; } = lastName;
	public string? LastName
	{
		get => LastNameJSON;
		set
		{
			LastNameJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("dateOfBirth")]
	public DateOnly DateOfBirthJSON { get; set; }
	public DateOnly DateOfBirth
	{
		get => DateOfBirthJSON;
		set
		{
			DateOfBirthJSON = value;
			Dirty = true;
		}
	}

	[JsonInclude]
	[JsonPropertyName("postalAddresses")]
	public List<PostalAddress> PostalAddresses = [];

	[JsonInclude]
	[JsonPropertyName("phoneNumbers")]
	public List<Phone> PhoneNumbers = [];
	public void AddPostalAddress(PostalAddress address)
	{
		this.PostalAddresses.Add(address);
	}
	public void RemovePostalAddress(PostalAddress address)
	{
		PostalAddresses.Remove(address);
	}

	public void AddPhoneNumber(Phone phone)
	{
		this.PhoneNumbers.Add(phone);
	}
}