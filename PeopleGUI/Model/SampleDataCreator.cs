namespace PeopleGUI.Model;

public static class SampleDataCreator
{
	public static List<Person> CreatePeople(int count)
	{
		Console.WriteLine("Creating " + count + " sample-persons");
		List<Person> persons = [];
		for( int i = 0; i < count; i++)
		{
			string suffix = i.ToString();
			Person p = new("ID." + suffix, "Last Name" + suffix, "First Name" + suffix);
			
			if( i > 1 )
			{
				p.AddPostalAddress(new PostalAddress().SetPostcode("01702").SetTown("Possendorf").SetStreet("Südhang").SetStreetNumber("20").Save());
				p.AddPhoneNumber(new Phone(49, 172, 6004535, Phone.PhoneType.Mobile));
			}
			if( i>=3 && i%3 == 0 )
			{
				p.AddPostalAddress(new PostalAddress().SetPostcode("01097").SetTown("Dresden").SetStreet("Buchenstr.").SetStreetNumber("19B").Save());
				p.AddPhoneNumber(new Phone(49, 35206, 261658, Phone.PhoneType.Home));
				p.AddPhoneNumber(new Phone(49, 351, 4108119, Phone.PhoneType.Office));
			}
			
			persons.Add(p);
		}
		return persons;
	}
}