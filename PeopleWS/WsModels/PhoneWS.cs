using PeopleWS.DB.Models;

namespace PeopleWS.WsModels;

public record PhoneWS(int? CountryPrefixWS, int? RegionPrefixWS, int? NumberWS, string? PhoneTypeWS)
{
	public static PhoneWS FromDbModel(Phone phone)
	{
		return new PhoneWS(
			phone.CountryPrefix == null ? 0 : int.Parse(phone.CountryPrefix),	//Parse nicely removes + and leading zeros :)
			phone.TownPrefix == null ? 0 : (int)phone.TownPrefix,
			phone.Number == null ? 0 : (int)phone.Number,
			phone.PhoneType);
	}
}