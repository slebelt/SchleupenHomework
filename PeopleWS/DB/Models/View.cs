using System;
using System.Collections.Generic;

namespace PeopleWS.DB.Models;

public partial class View
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string Addresses { get; set; } = null!;

    public string PhoneNumbers { get; set; } = null!;
}
