using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Package
{
    public int PackageId { get; set; }

    public int? TrainerId { get; set; }

    public string? Img { get; set; }

    public string PackageName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Cost { get; set; }

    public decimal? Discount { get; set; }

    public virtual ICollection<MemberPackage> MemberPackages { get; set; } = new List<MemberPackage>();

    public virtual Trainer? Trainer { get; set; }
}
