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
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (!(HttpContext.Session.GetString("role") == "1"))
                {
                    ViewData["Layout"] = GetLayout();
                    return View(await _context.ReservationsSet.ToListAsync());
                }
                else
                {
                    return RedirectToAction("IndexAdmin", "Reservations");
                }
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
        public async Task<IActionResult> IndexAdmin()
        {
            if (HttpContext.Session.GetString("role") == "1")
            {
                ViewData["Layout"] = GetLayout();
                return View(await _context.ReservationsSet.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Reservations");
            }
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (!(HttpContext.Session.GetString("role") == "1"))
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
                else
                {
                    return RedirectToAction("DetailsAdmin", "Reservations");
                }
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
        public async Task<IActionResult> DetailsAdmin(int? id)
        {
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
                if (HttpContext.Session.GetString("role") == "1")
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
                else
                {
                    return RedirectToAction("Details", "Reservations");
                }
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Reservations/Create
        public IActionResult Create(int flightId)
        {
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("FlightId", out buffer))
            {
                ViewData["Layout"] = GetLayout();
                return View();
            }
            else
                return RedirectToAction("Index", "Flights");
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,FirstName,Surename,LastName,EGN,PhoneNumber,Nationality,Email,TicketType")] Reservations reservations)
        {
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("FlightId",out buffer))
            {
                if (ModelState.IsValid)
                {

                    reservations.FlightId = int.Parse(HttpContext.Session.GetString("FlightId"));
                    _context.Add(reservations);
                    var flight = await _context.FlightsSet.FirstOrDefaultAsync(m => m.FlightId == reservations.FlightId);
                    if (reservations.TicketType == 1)
                    {
                        flight.VacantSpotsBussiness--;
                    }
                    else if (reservations.TicketType == 0)
                    {
                        flight.VacantSpots--;
                    }
                    if (flight.VacantSpots < 0)
                    {
                        ViewData["result"] = "Not enough space in economy!";
                        return View();
                    }
                    if (flight.VacantSpotsBussiness < 0)
                    {
                        ViewData["result"] = "Not enough space in bussiness!";
                        return View();
                    }
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(reservations);
            }
            else
                return RedirectToAction("Index","Flights");
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Layout"] = GetLayout();
            if (HttpContext.Session.GetString("role")=="1")
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
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,FirstName,Surename,LastName,EGN,PhoneNumber,Nationality,Email,TicketType")] Reservations reservations)
        {
            if (HttpContext.Session.GetString("role") == "1")
            {
                ViewData["Layout"] = GetLayout();
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
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Layout"] = GetLayout();
            if (HttpContext.Session.GetString("role") == "1")
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
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Layout"] = GetLayout();
            if (HttpContext.Session.GetString("role") == "1")
            {
                var reservations = await _context.ReservationsSet.FindAsync(id);
                _context.ReservationsSet.Remove(reservations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        private bool ReservationsExists(int id)
        {
            return _context.ReservationsSet.Any(e => e.ReservationId == id);
        }
        public string GetLayout()
        {
            byte[] buffer = new byte[200];
            if (HttpContext.Session.TryGetValue("username", out buffer))
            {
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
    }
}
