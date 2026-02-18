using PeopleWS.DB.Models;

namespace PeopleWS.WsModels;

public record PostalAddressWS(string PostalCodeWS, string TownWS, string StreetWS, string StreetNumberWS)
{
	public static PostalAddressWS FromDbModel(Address address)
	{
		return new PostalAddressWS(address.PostalCode.ToString(), address.Town, address.Street, address.StreetNumber);
	}
}