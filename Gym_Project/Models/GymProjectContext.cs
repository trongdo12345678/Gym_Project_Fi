using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gym_Project.Models;

public partial class GymProjectContext : DbContext
{
    public GymProjectContext()
    {
    }

    public GymProjectContext(DbContextOptions<GymProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<ClassPack> ClassPacks { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberPackage> MemberPackages { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=TRONGDO\\SQLEXPRESS;Initial Catalog=Gym_Project;Persist Security Info=True;User ID=sa;Password=trongdo123;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263CCFDCD9F5");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.TrainerId).HasColumnName("TrainerID");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("FK__Attendanc__Train__403A8C7D");
        });

        modelBuilder.Entity<ClassPack>(entity =>
        {
            entity.HasKey(e => e.ClassId);

            entity.ToTable("ClassPack");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.AssignmentTime).HasMaxLength(50);
            entity.Property(e => e.ClassName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Img).HasColumnType("text");
            entity.Property(e => e.TrainerId).HasColumnName("TrainerID");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF66C8E8418");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.FeedbackText).HasColumnType("text");
            entity.Property(e => e.TrainerId).HasColumnName("TrainerID");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("FK_Feedback_Trainer");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__0CF04B3838C52C79");

            entity.ToTable("Member");

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MemName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Members)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Member_Role");
        });

        modelBuilder.Entity<MemberPackage>(entity =>
        {
            entity.HasKey(e => e.MemPackId);

            entity.Property(e => e.MemPackId).HasColumnName("MemPackID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.PayId).HasColumnName("PayID");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TrainerId).HasColumnName("TrainerID");

            entity.HasOne(d => d.Class).WithMany(p => p.MemberPackages)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_MemberPackages_ClassPack");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberPackages)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_MemberPackages_Member");

            entity.HasOne(d => d.Package).WithMany(p => p.MemberPackages)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK_MemberPackages_Package");

            entity.HasOne(d => d.Pay).WithMany(p => p.MemberPackages)
                .HasForeignKey(d => d.PayId)
                .HasConstraintName("FK_MemberPackages_Payments");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Package__322035ECCA5EF84F");

            entity.ToTable("Package");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Img).HasColumnType("text");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TrainerId).HasColumnName("TrainerID");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Packages)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("FK_Package_Trainer");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PayId);

            entity.Property(e => e.PayId).HasColumnName("PayID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.PaymentDate).HasColumnName("paymentDate");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("paymentMethod");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("PK__Trainer__366A1B9C6AF712BF");

            entity.ToTable("Trainer");

            entity.Property(e => e.TrainerId).HasColumnName("TrainerID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Img).HasColumnType("text");
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TrainerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Trainers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Trainer_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
