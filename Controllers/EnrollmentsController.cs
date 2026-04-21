using BaiTapCSharp_MVC.Data;
using BaiTapCSharp_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaiTapCSharp_MVC.Controllers;

public class EnrollmentsController : Controller
{
    private readonly AppDbContext _context;

    public EnrollmentsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Enrollments.Include(e => e.ClassSection).Include(e => e.Student);
        return View(await appDbContext.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrollment = await _context.Enrollments
            .Include(e => e.ClassSection)
            .Include(e => e.Student)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (enrollment == null)
        {
            return NotFound();
        }

        return View(enrollment);
    }

    public IActionResult Create()
    {
        ViewData["ClassSectionId"] = new SelectList(_context.ClassSections, "Id", "Name");
        ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,StudentId,ClassSectionId")] Enrollment enrollment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClassSectionId"] = new SelectList(_context.ClassSections, "Id", "Name", enrollment.ClassSectionId);
        ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrollment.StudentId);
        return View(enrollment);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null)
        {
            return NotFound();
        }
        ViewData["ClassSectionId"] = new SelectList(_context.ClassSections, "Id", "Name", enrollment.ClassSectionId);
        ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrollment.StudentId);
        return View(enrollment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,ClassSectionId")] Enrollment enrollment)
    {
        if (id != enrollment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(enrollment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(enrollment.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClassSectionId"] = new SelectList(_context.ClassSections, "Id", "Name", enrollment.ClassSectionId);
        ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrollment.StudentId);
        return View(enrollment);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrollment = await _context.Enrollments
            .Include(e => e.ClassSection)
            .Include(e => e.Student)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (enrollment == null)
        {
            return NotFound();
        }

        return View(enrollment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment != null)
        {
            _context.Enrollments.Remove(enrollment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EnrollmentExists(int id)
    {
        return _context.Enrollments.Any(e => e.Id == id);
    }
}
