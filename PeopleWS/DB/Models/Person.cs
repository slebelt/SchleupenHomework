using System;
using System.Collections.Generic;

namespace PeopleWS.DB.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
