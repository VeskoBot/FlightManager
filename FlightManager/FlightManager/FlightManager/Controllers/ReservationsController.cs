using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data;
using FlightManager.Models;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FlightManager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly FlightsManagerDBContext _context;

        public ReservationsController(FlightsManagerDBContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReservationsSet.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.ReservationsSet
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);
        }

        // GET: Reservations/Create
        public IActionResult Create(int flightId)
        {
            flightId = int.Parse(HttpContext.Session.GetString("FlightId"));
            ViewData["id"] = flightId;
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,FirstName,Surename,LastName,EGN,PhoneNumber,Nationality,TicketType")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
             //   byte[] buffer = new byte[200];
              /// HttpContext.Session.TryGetValue("flightId", out buffer);
              /// if (BitConverter.IsLittleEndian)
             ///       Array.Reverse(buffer);

              ///  int flightId = BitConverter.ToInt32(buffer, 0);
              ///  reservations.FlightId = flightId;
                _context.Add(reservations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservations);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.ReservationsSet.FindAsync(id);
            if (reservations == null)
            {
                return NotFound();
            }
            return View(reservations);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,FirstName,Surename,LastName,EGN,PhoneNumber,Nationality,TicketType")] Reservations reservations)
        {
            if (id != reservations.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationsExists(reservations.ReservationId))
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
            return View(reservations);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.ReservationsSet
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservations = await _context.ReservationsSet.FindAsync(id);
            _context.ReservationsSet.Remove(reservations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationsExists(int id)
        {
            return _context.ReservationsSet.Any(e => e.ReservationId == id);
        }
    }
}
