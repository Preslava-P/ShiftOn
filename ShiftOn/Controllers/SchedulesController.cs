using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShiftOn.Models;

namespace ShiftOn.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly Context _context;

        public SchedulesController(Context context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var context = _context.Schedules.Include(s => s.Shift).Include(s => s.User);
            return View(await context.ToListAsync());
        }

        // GET: Schedules
        public async Task<IActionResult> Index(Guid userId)
        {
            var context = _context.Schedules
                .Where(sc => sc.UserId.Equals(userId))
                .Include(s => s.Shift)
                .Include(s => s.User);

            return View(await context.ToListAsync());
        }






        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Shift)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftName");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,ShiftId,UserId,VacationRequestId")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.ScheduleId = Guid.NewGuid();
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftName", schedule.ShiftId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", schedule.UserId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftName", schedule.ShiftId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", schedule.UserId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ScheduleId,ShiftId,UserId,VacationRequestId")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
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
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftName", schedule.ShiftId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", schedule.UserId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Shift)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Schedules == null)
            {
                return Problem("Entity set 'Context.Schedules'  is null.");
            }
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(Guid id)
        {
          return _context.Schedules.Any(e => e.ScheduleId == id);
        }
    }
}
