using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? HireDate { get; set; }

    public string? Status { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();
}
