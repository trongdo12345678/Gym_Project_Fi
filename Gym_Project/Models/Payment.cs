using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Payment
{
    public int PayId { get; set; }

    public int? MemberId { get; set; }

    public int? PackageId { get; set; }

    public decimal? Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public virtual ICollection<MemberPackage> MemberPackages { get; set; } = new List<MemberPackage>();

    public virtual Package? Package { get; set; }

    public virtual Member? Member { get; set; }
}
