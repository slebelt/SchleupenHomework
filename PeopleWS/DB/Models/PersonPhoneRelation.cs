using System;
using System.Collections.Generic;

namespace PeopleWS.DB.Models;

public partial class PersonPhoneRelation
{
    public int PersonId { get; set; }

    public int PhoneId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Phone Phone { get; set; } = null!;
}
