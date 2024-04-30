using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym_Project.Models;

public partial class MemberPackage
{
    public int MemPackId { get; set; }

    public int? TrainerId { get; set; }

    public int? MemberId { get; set; }

    public string? Status { get; set; }

    public int? PayId { get; set; }

    public int? ClassId { get; set; }

    public int? PackageId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ClassPack? Class { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Package? Package { get; set; }

    public virtual Trainer? Trainer { get; set; }

    public virtual Payment? Pay { get; set; }
}
