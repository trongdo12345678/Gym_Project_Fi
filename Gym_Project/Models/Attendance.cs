using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? TrainerId { get; set; }

    public DateOnly? Date { get; set; }

    public bool? Status { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
