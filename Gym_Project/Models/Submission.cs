using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public int? CompetitionId { get; set; }

    public int? ArtworkId { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public string? Award { get; set; }

    public int? Mark { get; set; }

    public int? ExhibitionId { get; set; }

    public string? Status { get; set; }

    public virtual Artwork? Artwork { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual Exhibition? Exhibition { get; set; }
}
