using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Competition
{
    public int CompetitionId { get; set; }

    public string? CompetitionName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public string? Img { get; set; }

    public string? Description { get; set; }

    public decimal? Awards { get; set; }

    public int? TeacherId { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual Teacher? Teacher { get; set; }
}
