using System;
using System.Collections.Generic;
namespace PeopleWS.DB.Models;
public partial class Address
{
    public int? AddressId { get; set; }

    public string? Street { get; set; }

    public string? StreetNumber { get; set; }

    public int? PostalCode { get; set; }

    public string? Town { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
