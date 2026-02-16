namespace PeopleGUI.Model;

public class Phone(int countryPrefix, int regionPrefix, int number, Phone.PhoneType phoneType)
{
	public enum PhoneType : ushort
	{
		Mobile = 0,
		Home = 1,
		Office = 2
	}
	public int countryPrefix = countryPrefix;
	public int regionPrefix = regionPrefix;
	public int number = number;
	public PhoneType phoneType = phoneType;

	public string GetNumberAsString()
	{
		return phoneType.ToString() + ": " + countryPrefix.ToString() + " " + regionPrefix.ToString() + " " + number.ToString();
	}
}