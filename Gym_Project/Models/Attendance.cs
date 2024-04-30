using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym_Project.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? NumberOfSessions { get; set; } = 1;
    [Required]
    public int? TrainerId { get; set; }

    public DateOnly? Date { get; set; }
    [Required]
    public bool? Status { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
