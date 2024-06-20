using System;
using System.Collections.Generic;

namespace Gym_Project.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Email { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public DateTime? Dob { get; set; }

    public string? Status { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string? Password { get; set; }

    public int? ClassId { get; set; }

    public virtual ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();

    public virtual Class? Class { get; set; }
}
