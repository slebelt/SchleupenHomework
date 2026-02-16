using System.Text.Json.Serialization;
namespace PeopleGUI.Model;

public class Phone()
{
	[JsonPropertyName("countryPrefix")]
	public int CountryPrefix { get; set; }

	[JsonPropertyName("regionPrefix")]
	public int RegionPrefix { get; set; }

	[JsonPropertyName("number")]
	public int Number { get; set; }

	[JsonPropertyName("phoneType")]
	public string PhoneType { get; set; } = "";
	public Phone SetCountyPrefix(int countryPrefix)
	{
		CountryPrefix = countryPrefix;
		return this;
	}
	public Phone SetRegionPrefix(int regionPrefix)
	{
		RegionPrefix = regionPrefix;
		return this;
	}
	public Phone SetNumber(int number)
	{
		Number = number;
		return this;
	}
	public Phone SetPhoneType(string phoneType)
	{
		PhoneType = phoneType;
		return this;
	}
	public string GetNumberAsString()
	{
		return PhoneType + ": " + CountryPrefix.ToString() + " " + RegionPrefix.ToString() + " " + Number.ToString();
	}
}