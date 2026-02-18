using System;
using System.Collections.Generic;

namespace PeopleWS.DB.Models;

public partial class PersonAddressRelation
{
    public int PersonId { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
