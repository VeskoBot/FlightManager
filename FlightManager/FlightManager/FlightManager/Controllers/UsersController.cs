using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Utilities;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FlightManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly FlightsManagerDBContext _context;

        public UsersController(FlightsManagerDBContext context)
        {
            _context = context;
        }
        public  IActionResult Login()
        {
            ViewData["result"] = "";
            ViewData["Layout"] = GetLayout();
            byte[] buffer = new byte[200];
            if (!HttpContext.Session.TryGetValue("username", out buffer))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Flights");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username,string pass)
        {
            ViewData["Layout"] = GetLayout();
            if (username == null || pass == null)
            {
                ViewData["result"] = "Enter a username and a password!";
                return View();
            }
            string hashPass = Security.ComputeSha256Hash(pass);
            var user = await _context.UsersSet
                .FirstOrDefaultAsync(m => m.Username == username && m.Password == hashPass);
            if (user == null)
            {
                ViewData["result"] = "Invalid username or password";
                return View();
            }
            HttpContext.Session.Set("username",Encoding.UTF8.GetBytes(user.Username));
            HttpContext.Session.Set("role", Encoding.UTF8.GetBytes(user.Role.ToString()));
            return RedirectToAction("Index","Flights");
        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("role")=="1")
            {
                ViewData["Layout"] = GetLayout();
                return View(await _context.UsersSet.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login","Users");
            }
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("role") == "1")
            {
                ViewData["Layout"] = GetLayout();
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _context.UsersSet
                    .FirstOrDefaultAsync(m => m.UserId == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            byte[] buffer = new byte[200];
            if (!HttpContext.Session.TryGetValue("username", out buffer))
            {
                ViewData["Layout"] = GetLayout();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Flights");
            }
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Password,Email,FirstName,LastName,EGN,Address,PhoneNumber,Role")] User user)
        {
            byte[] buffer = new byte[200];
            if (!HttpContext.Session.TryGetValue("username", out buffer))
            {
                ViewData["Layout"] = GetLayout();
                if (ModelState.IsValid)
                {
                    user.Password = Security.ComputeSha256Hash(user.Password);
                    if (!_context.UsersSet.Any())
                    {
                        user.Role = 1;
                    }
                    else
                    {
                        user.Role = 0;
                    }
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Flights");
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("role") == "1")
            {
                ViewData["Layout"] = GetLayout();
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _context.UsersSet.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            else{
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Password,Email,FirstName,LastName,EGN,Address,PhoneNumber,Role")] User user)
        {
            if (HttpContext.Session.GetString("role") == "1")
            {
                ViewData["Layout"] = GetLayout();
                if (id != user.UserId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.UserId))
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
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("role") == "1")
            {
                ViewData["Layout"] = GetLayout();
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _context.UsersSet
                    .FirstOrDefaultAsync(m => m.UserId == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("role") == "1")
            {
                ViewData["Layout"] = GetLayout();
                var user = await _context.UsersSet.FindAsync(id);
                _context.UsersSet.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }

        }

        private bool UserExists(int id)
        {
            return _context.UsersSet.Any(e => e.UserId == id);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Flights");
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
