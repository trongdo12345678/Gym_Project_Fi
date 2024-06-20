using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Artwork
{
    public int ArtworkId { get; set; }

    public string? ArtworkName { get; set; }

    public int? StudentId { get; set; }

    public string? FileUrl { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public string? Descritption { get; set; }

    public DateTime? DayPost { get; set; }

    public virtual Student? Student { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
