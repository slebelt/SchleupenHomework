using System.Text.Json.Serialization;

namespace PeopleGUI.Model;

public class Person(string? id, string? lastName, string? firstName)
{
	public bool Dirty { get; private set; } = false;
	[JsonPropertyName("id")]
	public string? ID { get; } = id;

	[JsonPropertyName("firstName")]
	public string? FirstName { get; set; } = firstName;

	[JsonPropertyName("lastName")]
	public string? LastName { get; set; } = lastName;

	[JsonPropertyName("dateOfBirth")]
	public DateOnly DateOfBirth { get; set; }

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