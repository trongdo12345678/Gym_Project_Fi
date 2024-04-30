using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym_Project.Models;

public partial class ClassPack
{
    public int ClassId { get; set; }
    [Required]
    public string? ClassName { get; set; }

    public DateOnly? DateClass { get; set; }
    [Required]
    public string? AssignmentTime { get; set; }

    public virtual ICollection<MemberPackage> MemberPackages { get; set; } = new List<MemberPackage>();
}
