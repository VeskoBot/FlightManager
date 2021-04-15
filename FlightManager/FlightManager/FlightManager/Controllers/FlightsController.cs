using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data;
using System.Text;
using FlightManager.Models;

namespace FlightManager.Controllers
{
    public class FlightsController : Controller
    {
        private readonly FlightsManagerDBContext _context;

        public FlightsController(FlightsManagerDBContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.FlightsSet.ToListAsync());
        }
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _context.FlightsSet.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpContext.Session.Set("FlightId", Encoding.UTF8.GetBytes(id.ToString()));

            var flights = await _context.FlightsSet
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flights == null)
            {
                return NotFound();
            }

            return View(flights);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,DepartureLoaction,ArrivalLocation,DepartureTime,ArrivalTime,AirplaneType,AirplaneCode,VacantSpots,VacantSpotsBussiness")] Flights flights)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flights);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flights);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var flights = await _context.FlightsSet.FindAsync(id);
                if (flights == null)
                {
                    return NotFound();
                }
                return View(flights);
            }
            else
            {
                return RedirectToAction("Login","Users");
            }     
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,DepartureLoaction,ArrivalLocation,DepartureTime,ArrivalTime,AirplaneType,AirplaneCode,VacantSpots,VacantSpotsBussiness")] Flights flights)
        {
            if (id != flights.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flights);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightsExists(flights.FlightId))
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
            return View(flights);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flights = await _context.FlightsSet
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flights == null)
            {
                return NotFound();
            }

            return View(flights);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flights = await _context.FlightsSet.FindAsync(id);
            _context.FlightsSet.Remove(flights);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightsExists(int id)
        {
            return _context.FlightsSet.Any(e => e.FlightId == id);
        }
    }
}
