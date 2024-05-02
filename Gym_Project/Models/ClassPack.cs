using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class ClassPack
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public int? TrainerId { get; set; }

    public DateOnly? DateClass { get; set; }

    public string? AssignmentTime { get; set; }

    public virtual ICollection<MemberPackage> MemberPackages { get; set; } = new List<MemberPackage>();

    public virtual Trainer? Trainer { get; set; }
}
