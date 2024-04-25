using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? MemName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? RoleId { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<MemberPackage> MemberPackages { get; set; } = new List<MemberPackage>();

    public virtual Role? Role { get; set; }
}
