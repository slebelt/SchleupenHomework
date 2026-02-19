using PeopleWS.DB.Models;

namespace PeopleWS.WsModels;

public record PostalAddressWS(int? ID, string? PostalCodeWS, string? TownWS, string? StreetWS, string? StreetNumberWS)
{
	public Address ToDbModel()
	{
		Address addressDB = new();
		addressDB.AddressId = ID;
		UpdateDbModel(addressDB);
		return addressDB;
	}
	public void UpdateDbModel(Address existingAddress)
	{
		existingAddress.Street = StreetWS;
		existingAddress.StreetNumber = StreetNumberWS;
		existingAddress.Town = TownWS;
		existingAddress.PostalCode = PostalCodeWS == null ? null : int.Parse(PostalCodeWS);
	}
	public static PostalAddressWS FromDbModel(Address address)
	{
		return new PostalAddressWS(address.AddressId, address.PostalCode.ToString(), address.Town, address.Street, address.StreetNumber);
	}
}