using BaiTapCSharp_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BaiTapCSharp_MVC.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Students.AnyAsync())
        {
            return;
        }

        var teachers = new List<Teacher>
        {
            new() { Id = "T01", Name = "Nguyen Van A", Age = 40 },
            new() { Id = "T02", Name = "Tran Thi B", Age = 35 }
        };

        var students = new List<Student>
        {
            new() { Id = "S01", Name = "Le Minh", Age = 20 },
            new() { Id = "S02", Name = "Pham An", Age = 21 },
            new() { Id = "S03", Name = "Do Hai", Age = 19 }
        };

        var courses = new List<Course>
        {
            new() { Id = "C01", Name = "ASP.NET Core", TeacherId = "T01" },
            new() { Id = "C02", Name = "Database Systems", TeacherId = "T02" }
        };

        var enrollments = new List<CoursesManegement>
        {
            new() { CourseId = "C01", StudentId = "S01" },
            new() { CourseId = "C01", StudentId = "S02" },
            new() { CourseId = "C02", StudentId = "S02" },
            new() { CourseId = "C02", StudentId = "S03" }
        };

        await context.Teachers.AddRangeAsync(teachers);
        await context.Students.AddRangeAsync(students);
        await context.Courses.AddRangeAsync(courses);
        await context.CoursesManegements.AddRangeAsync(enrollments);

        await context.SaveChangesAsync();
    }
}
