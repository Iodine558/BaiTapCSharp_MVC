using BaiTapCSharp_MVC.Data;
using BaiTapCSharp_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaiTapCSharp_MVC.Controllers;

public class ClassSectionsController : Controller
{
    private readonly AppDbContext _context;

    public ClassSectionsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.ClassSections.Include(c => c.Course).Include(c => c.Teacher);
        return View(await appDbContext.ToListAsync());
    }

    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var classSection = await _context.ClassSections
            .Include(c => c.Course)
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (classSection == null)
        {
            return NotFound();
        }

        return View(classSection);
    }

    public IActionResult Create()
    {
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
        ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,CourseId,TeacherId")] ClassSection classSection)
    {
        if (ModelState.IsValid)
        {
            _context.Add(classSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", classSection.CourseId);
        ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", classSection.TeacherId);
        return View(classSection);
    }

    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var classSection = await _context.ClassSections.FindAsync(id);
        if (classSection == null)
        {
            return NotFound();
        }
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", classSection.CourseId);
        ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", classSection.TeacherId);
        return View(classSection);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Name,CourseId,TeacherId")] ClassSection classSection)
    {
        if (id != classSection.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(classSection);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSectionExists(classSection.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", classSection.CourseId);
        ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", classSection.TeacherId);
        return View(classSection);
    }

    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var classSection = await _context.ClassSections
            .Include(c => c.Course)
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (classSection == null)
        {
            return NotFound();
        }

        return View(classSection);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var classSection = await _context.ClassSections.FindAsync(id);
        if (classSection != null)
        {
            _context.ClassSections.Remove(classSection);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClassSectionExists(string id)
    {
        return _context.ClassSections.Any(e => e.Id == id);
    }
}
