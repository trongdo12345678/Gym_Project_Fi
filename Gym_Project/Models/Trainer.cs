using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Trainer
{
    public int TrainerId { get; set; }

    public string? Img { get; set; }

    public string? Password { get; set; }

    public string? Username { get; set; }

    public string? TrainerName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? RoleId { get; set; }

    public string? Specialization { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    public virtual Role? Role { get; set; }
}
