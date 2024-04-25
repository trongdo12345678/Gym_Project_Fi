using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? TrainerId { get; set; }

    public int? Rating { get; set; }

    public string? FeedbackText { get; set; }

    public DateOnly? DateFeed { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
