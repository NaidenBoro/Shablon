using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public enum Roles
{
    Client,
    HouseKeeper,
    Admin
}

public class ShablonUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}

