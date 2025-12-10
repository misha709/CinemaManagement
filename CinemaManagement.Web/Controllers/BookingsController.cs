using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaManagement.Web.Data;
using CinemaManagement.Web.Models;

namespace CinemaManagement.Web.Controllers;

public class BookingsController : Controller
{
    private readonly CinemaContext _context;

    public BookingsController(CinemaContext context)
    {
        _context = context;
    }

    // GET: Bookings
    public async Task<IActionResult> Index()
    {
        var bookings = await _context.Bookings
            .Include(b => b.Screening)
                .ThenInclude(s => s.Movie)
            .Include(b => b.Screening)
                .ThenInclude(s => s.Hall)
            .ToListAsync();
        return View(bookings);
    }

    // GET: Bookings/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var booking = await _context.Bookings
            .Include(b => b.Screening)
                .ThenInclude(s => s.Movie)
            .Include(b => b.Screening)
                .ThenInclude(s => s.Hall)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    // GET: Bookings/Create
    public IActionResult Create()
    {
        var screenings = _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .Select(s => new
            {
                s.Id,
                Display = $"{s.Movie.Title} - {s.Hall.Name} - {s.StartTime:g}"
            })
            .ToList();

        ViewData["ScreeningId"] = new SelectList(screenings, "Id", "Display");
        return View();
    }

    // POST: Bookings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ScreeningId,CustomerName,CustomerEmail,RowNumber,SeatNumber,Status")] Booking booking)
    {
        if (ModelState.IsValid)
        {
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var screenings = _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .Select(s => new
            {
                s.Id,
                Display = $"{s.Movie.Title} - {s.Hall.Name} - {s.StartTime:g}"
            })
            .ToList();

        ViewData["ScreeningId"] = new SelectList(screenings, "Id", "Display", booking.ScreeningId);
        return View(booking);
    }

    // GET: Bookings/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        var screenings = _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .Select(s => new
            {
                s.Id,
                Display = $"{s.Movie.Title} - {s.Hall.Name} - {s.StartTime:g}"
            })
            .ToList();

        ViewData["ScreeningId"] = new SelectList(screenings, "Id", "Display", booking.ScreeningId);
        return View(booking);
    }

    // POST: Bookings/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ScreeningId,CustomerName,CustomerEmail,RowNumber,SeatNumber,Status")] Booking booking)
    {
        if (id != booking.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.Id))
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

        var screenings = _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .Select(s => new
            {
                s.Id,
                Display = $"{s.Movie.Title} - {s.Hall.Name} - {s.StartTime:g}"
            })
            .ToList();

        ViewData["ScreeningId"] = new SelectList(screenings, "Id", "Display", booking.ScreeningId);
        return View(booking);
    }

    // GET: Bookings/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var booking = await _context.Bookings
            .Include(b => b.Screening)
                .ThenInclude(s => s.Movie)
            .Include(b => b.Screening)
                .ThenInclude(s => s.Hall)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    // POST: Bookings/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            _context.Bookings.Remove(booking);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookingExists(int id)
    {
        return _context.Bookings.Any(e => e.Id == id);
    }
}
