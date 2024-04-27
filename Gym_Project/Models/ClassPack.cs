using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class ClassPack
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public DateOnly? DateClass { get; set; }

    public int? MemberId { get; set; }

    public int? MemPackId { get; set; }

    public int? TrainerId { get; set; }

    public string? AssignmentTime { get; set; }

    public int? PackageId { get; set; }

    public virtual MemberPackage? MemPack { get; set; }
}
