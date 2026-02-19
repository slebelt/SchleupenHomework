using System;
using System.Collections.Generic;

namespace PeopleWS.DB.Models;

public partial class Phone
{
    public int? PhoneId { get; set; }

    public string? PhoneType { get; set; }

    public string? CountryPrefix { get; set; }

    public int? TownPrefix { get; set; }

    public int? Number { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
