using BaiTapCSharp_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BaiTapCSharp_MVC.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<ClassSection> ClassSections { get; set; } = null!;
    public DbSet<Enrollment> Enrollments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Teacher>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Course>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<ClassSection>()
            .HasKey(cs => cs.Id);

        modelBuilder.Entity<Enrollment>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<ClassSection>()
            .HasOne(cs => cs.Course)
            .WithMany(c => c.ClassSections)
            .HasForeignKey(cs => cs.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClassSection>()
            .HasOne(cs => cs.Teacher)
            .WithMany(t => t.ClassSections)
            .HasForeignKey(cs => cs.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.ClassSection)
            .WithMany(cs => cs.Enrollments)
            .HasForeignKey(e => e.ClassSectionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.ClassSectionId })
            .IsUnique();
    }
}
