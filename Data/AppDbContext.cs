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
    public DbSet<CoursesManegement> CoursesManegements { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Teacher>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Course>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Course>()
            .HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CoursesManegement>()
            .HasKey(cm => new { cm.CourseId, cm.StudentId });

        modelBuilder.Entity<CoursesManegement>()
            .HasOne<Course>()
            .WithMany()
            .HasForeignKey(cm => cm.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CoursesManegement>()
            .HasOne<Student>()
            .WithMany()
            .HasForeignKey(cm => cm.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
