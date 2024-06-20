using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Account
{
    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
