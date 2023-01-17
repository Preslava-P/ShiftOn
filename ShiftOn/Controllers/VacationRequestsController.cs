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
    public class VacationRequestsController : Controller
    {
        private readonly Context _context;

        public VacationRequestsController(Context context)
        {
            _context = context;
        }

        // GET: VacationRequests
        public async Task<IActionResult> Index()
        {
            var context = _context.VacationRequests.Include(v => v.Schedule).Include(v => v.User);
            return View(await context.ToListAsync());
        }

        // GET: VacationRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VacationRequests == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequests
                .Include(v => v.Schedule)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VacationRequestId == id);
            if (vacationRequest == null)
            {
                return NotFound();
            }

            return View(vacationRequest);
        }

        // GET: VacationRequests/Create
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "ScheduleId", "ScheduleId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName");
            return View();
        }

        // POST: VacationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VacationRequestId,RequestDate,UserId,ScheduleId")] VacationRequest vacationRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "ScheduleId", "ScheduleId", vacationRequest.ScheduleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", vacationRequest.UserId);
            return View(vacationRequest);
        }

        // GET: VacationRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VacationRequests == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequests.FindAsync(id);
            if (vacationRequest == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "ScheduleId", "ScheduleId", vacationRequest.ScheduleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", vacationRequest.UserId);
            return View(vacationRequest);
        }

        // POST: VacationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VacationRequestId,RequestDate,UserId,ScheduleId")] VacationRequest vacationRequest)
        {
            if (id != vacationRequest.VacationRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationRequestExists(vacationRequest.VacationRequestId))
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "ScheduleId", "ScheduleId", vacationRequest.ScheduleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", vacationRequest.UserId);
            return View(vacationRequest);
        }

        // GET: VacationRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VacationRequests == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequests
                .Include(v => v.Schedule)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VacationRequestId == id);
            if (vacationRequest == null)
            {
                return NotFound();
            }

            return View(vacationRequest);
        }

        // POST: VacationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VacationRequests == null)
            {
                return Problem("Entity set 'Context.VacationRequests'  is null.");
            }
            var vacationRequest = await _context.VacationRequests.FindAsync(id);
            if (vacationRequest != null)
            {
                _context.VacationRequests.Remove(vacationRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationRequestExists(int id)
        {
          return _context.VacationRequests.Any(e => e.VacationRequestId == id);
        }
    }
}
