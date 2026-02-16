using System.Text.Json.Serialization;
namespace PeopleGUI.Model;

public class PostalAddress
{
	public bool Dirty { get; private set; } = true;

	private int? postcode = null;

	[JsonPropertyName("postalCode")]
	public string? Postcode
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
				Dirty = true;
			}
			catch (FormatException) { }
		}
	}

	private string? town = "";

	[JsonPropertyName("town")]
	public string? Town
	{
		get => town;
		set
		{
			town = value;
			Dirty = true;
		}
	}

	private string? street = "";

	[JsonPropertyName("street")]
	public string? Street
	{
		get => street;
		set
		{
			street = value;
			Dirty = true;
		}
	}

	private string? streetNumber = "";

	[JsonPropertyName("streetNumber")]
	public string? StreetNumber
	{
		get => streetNumber;
		set
		{
			streetNumber = value;
			Dirty = true;
		}
	}
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