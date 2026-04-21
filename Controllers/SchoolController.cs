using BaiTapCSharp_MVC.Data;
using BaiTapCSharp_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiTapCSharp_MVC.Controllers;

public class SchoolController : Controller
{
    private readonly AppDbContext _context;

    public SchoolController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new SchoolDataViewModel
        {
            Students = await _context.Students.OrderBy(s => s.Id).ToListAsync(),
            Teachers = await _context.Teachers.OrderBy(t => t.Id).ToListAsync(),
            Courses = await _context.Courses.OrderBy(c => c.Id).ToListAsync(),
            Enrollments = await (
                from e in _context.Enrollments
                join cs in _context.ClassSections on e.ClassSectionId equals cs.Id
                join c in _context.Courses on cs.CourseId equals c.Id
                join s in _context.Students on e.StudentId equals s.Id
                join t in _context.Teachers on cs.TeacherId equals t.Id
                orderby c.Id, s.Id
                select new CourseEnrollmentViewModel
                {
                    CourseName = c.Name,
                    StudentName = s.Name,
                    TeacherName = t.Name
                }
            ).ToListAsync()
        };

        return View(model);
    }
}
