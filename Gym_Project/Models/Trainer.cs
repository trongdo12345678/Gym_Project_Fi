using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym_Project.Models;

public partial class Trainer
{
    public int TrainerId { get; set; }
    [Required]
    public string? Img { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? TrainerName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }
    [Required]
    public int? RoleId { get; set; }
    [Required]
    public string? Specialization { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    public virtual Role? Role { get; set; }
}