using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
}
