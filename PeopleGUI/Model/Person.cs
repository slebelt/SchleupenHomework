namespace PeopleGUI.Model;
public class Person(string id, string lastName, string firstName)
{
	public string ID {get;} = id;
	public string FirstName { get; set; } = firstName;
	public string LastName { get; set; } = lastName;
	public DateOnly DateOfBirth { get; set; }
	public List<PostalAddress> PostalAddresses = [];
	public List<Phone> PhoneNumbers = [];

	public void AddPostalAddress( PostalAddress address )
	{
		this.PostalAddresses.Add(address);
	}
	public void RemovePostalAddress( PostalAddress address )
	{
		PostalAddresses.Remove(address);
	}

	public void AddPhoneNumber( Phone phone )
	{
		this.PhoneNumbers.Add(phone);
	}

}
