using PeopleWS.DB.Models;

namespace PeopleWS.WsModels;

public record PhoneWS(int CountryPrefixWS, int RegionPrefixWS, int NumberWS, string PhoneTypeWS)
{
	public static PhoneWS FromDbModel(Phone phone)
	{
		return new PhoneWS(int.Parse(phone.CountryPrefix), (int)phone.TownPrefix, (int)phone.Number, phone.PhoneType);
	}
}