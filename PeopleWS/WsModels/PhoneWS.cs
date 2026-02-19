using PeopleWS.DB.Models;

namespace PeopleWS.WsModels;

public record PhoneWS(int? ID, int? CountryPrefixWS, int? RegionPrefixWS, int? NumberWS, string? PhoneTypeWS)
{
	public Phone ToDbModel()
	{
		Phone phoneDB = new();
		phoneDB.PhoneId = ID;
		UpdateDbModel(phoneDB);
		return phoneDB;
	}
	public void UpdateDbModel(Phone existingPhone)
	{
		existingPhone.CountryPrefix = CountryPrefixWS?.ToString();
		existingPhone.TownPrefix = RegionPrefixWS;
		existingPhone.Number = NumberWS;
		existingPhone.PhoneType = PhoneTypeWS;
	}
	public static PhoneWS FromDbModel(Phone phone)
	{
		return new PhoneWS(
			phone.PhoneId,
			phone.CountryPrefix == null ? 0 : int.Parse(phone.CountryPrefix),	//Parse nicely removes + and leading zeros :)
			phone.TownPrefix == null ? 0 : (int)phone.TownPrefix,
			phone.Number == null ? 0 : (int)phone.Number,
			phone.PhoneType);
	}
}