using System.Text.Json.Serialization;
namespace PeopleGUI.Model;

public class PostalAddress
{
	public bool Dirty { get; private set; } = false;

	private int? postcode = null;
	public string? Postcode
	{
		get => PostcodeJSON;
		set
		{
			PostcodeJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("postalCode")]
	public string? PostcodeJSON
	{
		get
		{
			if (postcode == null)
			{
				return "";
			}
			return $"{postcode:D5}";
		}
		set
		{
			if (value == null || value.Length < 4 || value.Length > 5)
			{
				return;
			}
			try
			{
				postcode = Int32.Parse(value);
			}
			catch (FormatException) { }
		}
	}

	private string? town = "";
	public string? Town
	{
		get => TownJSON;
		set
		{
			TownJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("town")]
	public string? TownJSON{
		get => town;
		set
		{
			town = value;
		}
	}

	private string? street = "";
	public string? Street
	{
		get => StreetJSON;
		set
		{
			StreetJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("street")]
	public string? StreetJSON
	{
		get => street;
		set
		{
			street = value;
		}
	}

	private string? streetNumber = "";
	public string? StreetNumber
	{
		get => StreetNumberJSON;
		set
		{
			StreetNumberJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("streetNumber")]
	public string? StreetNumberJSON
	{
		get => streetNumber;
		set
		{
			streetNumber = value;
		}
	}

	//special getters and setters
	public string GetPostcodeAsString()
	{
		if (Postcode == null)
		{
			return "";
		}
		return $"{Postcode:D5}";
	}
	public PostalAddress SetPostcode(string postcode)
	{
		Postcode = postcode;
		return this;
	}
	public PostalAddress SetTown(string town)
	{
		Town = town;
		return this;
	}
	public PostalAddress SetStreet(string street)
	{
		Street = street;
		return this;
	}
	public PostalAddress SetStreetNumber(string streetNumber)
	{
		StreetNumber = streetNumber;
		return this;
	}
	public PostalAddress Save()
	{
		if (Postcode == "" && Town == "" && Street == "" && StreetNumber == "")
		{
			return this;
		}
		Dirty = false;
		return this;
	}
}