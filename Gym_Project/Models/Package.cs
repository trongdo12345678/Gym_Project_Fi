using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym_Project.Models;

public partial class Package
{
    public int PackageId { get; set; }
	[Required]
	public int? TrainerId { get; set; }
	[Required]
	public string? Img { get; set; }
	[Required]
	public string? PackageName { get; set; }
	[Required]
	public string? Description { get; set; }
	[Required]
	public decimal? Cost { get; set; }
	[Required]
	public decimal? Discount { get; set; }

    public virtual ICollection<MemberPackage> MemberPackages { get; set; } = new List<MemberPackage>();

    public virtual Trainer? Trainer { get; set; }
}
