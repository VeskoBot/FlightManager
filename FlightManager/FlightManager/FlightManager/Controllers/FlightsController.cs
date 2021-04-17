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
using Microsoft.AspNetCore.Http;

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
            if (HttpContext.Session.GetString("role") == "1")
            {
                return RedirectToAction("IndexAdmin", "Flights");
            }
            ViewData["Layout"] = GetLayout();
            return View(await _context.FlightsSet.ToListAsync());

        }
        public async Task<IActionResult> IndexAdmin()
        {
            if (HttpContext.Session.GetString("role") == "1")
            {
                ViewData["Layout"] = GetLayout();
                return View(await _context.FlightsSet.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Flights");
            }
        }

    // GET: Flights/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            ViewData["Layout"] = GetLayout();
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
            if (HttpContext.Session.GetString("role") == "1")
            {
                return RedirectToAction("IndexAdmin", "Flights");
            }
            else
            {
                ViewData["Length"] = (flights.ArrivalTime - flights.DepartureTime).ToString();
                return View(flights);
            }
        }
        public async Task<IActionResult> DetailsAdmin(int? id)
        {
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
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
                if (HttpContext.Session.GetString("role") != "1")
                {
                    return RedirectToAction("Index", "Flights");
                }
                else
                {
                    ViewData["Length"] = (flights.ArrivalTime - flights.DepartureTime).ToString();
                    return View(flights);
                }
            }
            else
            {
                return RedirectToAction("Index", "Flights");
            }
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (HttpContext.Session.GetString("role") != "1")
                {
                    return RedirectToAction("Index", "Flights");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,DepartureLoaction,ArrivalLocation,DepartureTime,ArrivalTime,AirplaneType,AirplaneCode,VacantSpots,VacantSpotsBussiness")] Flights flights)
        {
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (ModelState.IsValid)
                {
                    if (HttpContext.Session.GetString("role") != "1")
                    {
                        return RedirectToAction("Index", "Flights");
                    }
                    _context.Add(flights);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(flights);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (HttpContext.Session.GetString("role") != "1")
                {
                    return RedirectToAction("Index", "Flights");
                }
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
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (HttpContext.Session.GetString("role") != "1")
                {
                    return RedirectToAction("Index", "Flights");
                }
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
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (HttpContext.Session.GetString("role") != "1")
                {
                    return RedirectToAction("Index", "Flights");
                }
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
                ViewData["Length"] = (flights.ArrivalTime - flights.DepartureTime).ToString();
                return View(flights);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (HttpContext.Session.GetString("role") != "1")
                {
                    return RedirectToAction("Index", "Flights");
                }
                var flights = await _context.FlightsSet.FindAsync(id);
                _context.FlightsSet.Remove(flights);
                var reservations = await _context.ReservationsSet.ToListAsync();
                foreach(var el in reservations)
                {
                    if (el.FlightId == flights.FlightId)
                    {
                        _context.ReservationsSet.Remove(el);
                    }                  
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
        public string GetLayout()
        {
            byte[] buffer = new byte[200];
            if(HttpContext.Session.TryGetValue("username",out buffer)){
                if (HttpContext.Session.GetString("role") == "1")
                {
                    return "_LayoutAdmin";
                }
                else
                {
                    return "_LayoutUser";
                }
            }
            else
            {
                return "_Layout";
            }
        }

        private bool FlightsExists(int id)
        {
            return _context.FlightsSet.Any(e => e.FlightId == id);
        }

    }
}
