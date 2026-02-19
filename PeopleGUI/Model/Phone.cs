using System.Text.Json.Serialization;
namespace PeopleGUI.Model;

public class Phone()
{
	public bool Dirty { get; set; } = false;

	[JsonPropertyName("id")]
	public int? ID { get; set; }

	[JsonPropertyName("countryPrefixWS")]
	public int CountryPrefixJSON { get; set; }
	public int CountryPrefix
	{
		get => CountryPrefixJSON;
		set
		{
			CountryPrefixJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("regionPrefixWS")]
	public int RegionPrefixJSON { get; set; }
	public int RegionPrefix
	{
		get => RegionPrefixJSON;
		set
		{
			RegionPrefixJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("numberWS")]
	public int NumberJSON { get; set; }
	public int Number
	{
		get => NumberJSON;
		set
		{
			NumberJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("phoneTypeWS")]
	public string PhoneTypeJSON { get; set; } = "";
	public string PhoneType
	{
		get => PhoneTypeJSON;
		set
		{
			PhoneTypeJSON = value;
			Dirty = true;
		}
	}


	//special getters and setters
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
		return "+" + CountryPrefix.ToString() + " " + RegionPrefix.ToString() + " " + Number.ToString();
	}


	private static string IntToString(int value)
	{
		if (value == 0)
		{
			return "";
		}
		return value.ToString();
	}
	private static int MakeInt(string value)
	{
		try
		{
			return int.Parse(value);
		}
		catch (FormatException)
		{
			return 0;
		}
	}
	public string CountryPrefixStr
	{
		get
		{
			return Phone.IntToString(CountryPrefix);
		}
		set
		{
			CountryPrefix = Phone.MakeInt(value);
		}
	}
	public string RegionPrefixStr
	{
		get
		{
			return Phone.IntToString(RegionPrefix);
		}
		set
		{
			RegionPrefix = Phone.MakeInt(value);
		}
	}
	public string NumberStr
	{
		get
		{
			return Phone.IntToString(Number);
		}
		set
		{
			Number = Phone.MakeInt(value);
		}
	}
}