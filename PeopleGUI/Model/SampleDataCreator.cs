namespace PeopleGUI.Model;

public static class SampleDataCreator
{
	public static List<Person> CreatePeople(int count)
	{
		Console.WriteLine("Creating " + count + " sample-persons");
		List<Person> persons = [];
		for (int i = 0; i < count; i++)
		{
			string suffix = i.ToString();
			Person p = new("ID." + suffix, "Last Name" + suffix, "First Name" + suffix);

			if (i > 1)
			{
				p.AddPostalAddress(new PostalAddress().SetPostcode("01702").SetTown("Possendorf").SetStreet("Südhang").SetStreetNumber("20").Save());
				p.AddPhoneNumber(new Phone().SetCountyPrefix(49).SetRegionPrefix(172).SetNumber(6004535).SetPhoneType("Mobile"));
			}
			if (i >= 3 && i % 3 == 0)
			{
				p.AddPostalAddress(new PostalAddress().SetPostcode("01097").SetTown("Dresden").SetStreet("Buchenstr.").SetStreetNumber("19B").Save());
				p.AddPhoneNumber(new Phone().SetCountyPrefix(49).SetRegionPrefix(35206).SetNumber(261658).SetPhoneType("Home"));
				p.AddPhoneNumber(new Phone().SetCountyPrefix(49).SetRegionPrefix(351).SetNumber(4108119).SetPhoneType("Office"));
			}

			persons.Add(p);
		}
		return persons;
	}
}