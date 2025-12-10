using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaManagement.Web.Data;
using CinemaManagement.Web.Models;

namespace CinemaManagement.Web.Controllers;

public class ScreeningsController : Controller
{
    private readonly CinemaContext _context;

    public ScreeningsController(CinemaContext context)
    {
        _context = context;
    }

    // GET: Screenings
    public async Task<IActionResult> Index()
    {
        var screenings = await _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .ToListAsync();
        return View(screenings);
    }

    // GET: Screenings/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var screening = await _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (screening == null)
        {
            return NotFound();
        }

        return View(screening);
    }

    // GET: Screenings/Create
    public IActionResult Create()
    {
        ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title");
        ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Name");
        return View();
    }

    // POST: Screenings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,MovieId,HallId,StartTime,BasePrice")] Screening screening)
    {
        if (ModelState.IsValid)
        {
            _context.Add(screening);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", screening.MovieId);
        ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Name", screening.HallId);
        return View(screening);
    }

    // GET: Screenings/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var screening = await _context.Screenings.FindAsync(id);
        if (screening == null)
        {
            return NotFound();
        }
        ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", screening.MovieId);
        ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Name", screening.HallId);
        return View(screening);
    }

    // POST: Screenings/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,HallId,StartTime,BasePrice")] Screening screening)
    {
        if (id != screening.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(screening);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreeningExists(screening.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", screening.MovieId);
        ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Name", screening.HallId);
        return View(screening);
    }

    // GET: Screenings/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var screening = await _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (screening == null)
        {
            return NotFound();
        }

        return View(screening);
    }

    // POST: Screenings/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var screening = await _context.Screenings.FindAsync(id);
        if (screening != null)
        {
            _context.Screenings.Remove(screening);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ScreeningExists(int id)
    {
        return _context.Screenings.Any(e => e.Id == id);
    }
}
