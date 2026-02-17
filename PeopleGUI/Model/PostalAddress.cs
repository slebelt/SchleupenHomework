using System.Text.Json.Serialization;
namespace PeopleGUI.Model;

public class PostalAddress
{
	public bool Dirty { get; private set; } = false;

	private int? postcode = null;
	[JsonPropertyName("postalCodeWS")]
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
	public string? Postcode
	{
		get => PostcodeJSON;
		set
		{
			PostcodeJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("townWS")]
	public string? TownJSON { set; get; }
	public string? Town
	{
		get => TownJSON;
		set
		{
			TownJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("streetWS")]
	public string? StreetJSON { set; get; }
	public string? Street
	{
		get => StreetJSON;
		set
		{
			StreetJSON = value;
			Dirty = true;
		}
	}

	[JsonPropertyName("streetNumberWS")]
	public string? StreetNumberJSON { set; get; }
	public string? StreetNumber
	{
		get => StreetNumberJSON;
		set
		{
			StreetNumberJSON = value;
			Dirty = true;
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