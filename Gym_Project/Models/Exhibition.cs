using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Exhibition
{
    public int ExhibitionId { get; set; }

    public DateTime? StartDate { get; set; }

    public string? Status { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Description { get; set; }

    public string? ExhibitionName { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
