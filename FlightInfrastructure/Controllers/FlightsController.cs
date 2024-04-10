using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightDomain.Model;
using FlightInfrastructure;
using NuGet.DependencyResolver;
using System.Net.Sockets;

namespace FlightInfrastructure.Controllers
{
    public class FlightsController : Controller
    {
        private readonly DbflightsContext _context;

        public FlightsController(DbflightsContext context)
        {
            _context = context;
        }

        // GET: Flights
          public async Task<IActionResult> Index(int? id, string? name)
          { 

              var dbflightsContext = _context.Flights.Include(f => f.ArrivalAiroportNavigation).Include(f => f.DepartureAiroportNavigation);

              return View(await dbflightsContext.ToListAsync());


        }



        // GET : DepartureAirport

        public async Task<IActionResult> DepartureAirport()
        {

            ViewData["Departure Airport"] = new SelectList(_context.Flights, "Id", "Name");
            return View();
        }

        // GET: ArrivalAirport

        public async Task<IActionResult> ArrivalAirport()
        {

            ViewData["Arrival Airport"] = new SelectList(_context.Flights, "Id", "Name");
            return View();
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.ArrivalAiroportNavigation)
                .Include(f => f.DepartureAiroportNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["ArrivalAiroport"] = new SelectList(_context.Airports, "Id", "Name");
            ViewData["DepartureAiroport"] = new SelectList(_context.Airports, "Id", "Name");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create ([Bind("Id,Name,Date,Description,Duration,DepartureAiroport,ArrivalAiroport")] Flight flight)
        {

            var existingFlight = await _context.Flights.FirstOrDefaultAsync(c => c.Name== flight.Name);
            if (existingFlight != null)
            {
                ModelState.AddModelError("Name", "Такий авіарейс вже існує");
                return View(flight);
            }

            if (ModelState.IsValid)
              {
                  _context.Add(flight);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
              }

             if (flight.Date.Year < 1934 || flight.Date.Year > DateTime.Now.Year - 20)
                {
                ModelState.AddModelError("Date", "Дата авіарейсу повинна бути валідна");
                }

              ViewData["ArrivalAiroport"] = new SelectList(_context.Airports, "Id", "Name", flight.ArrivalAiroport);
              ViewData["DepartureAiroport"] = new SelectList(_context.Airports, "Id", "Name", flight.DepartureAiroport);
              return View(flight);
          }


    // GET: Flights/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            if (flight.Date.Year < 1934 || flight.Date.Year > DateTime.Now.Year - 20)
            {
                ModelState.AddModelError("Date", "Дата авіарейсу повинна бути валідна");
            }

            ViewData["ArrivalAiroport"] = new SelectList(_context.Airports, "Id", "Name", flight.ArrivalAiroport);
            ViewData["DepartureAiroport"] = new SelectList(_context.Airports, "Id", "Name", flight.DepartureAiroport);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,Description,Duration,DepartureAiroport,ArrivalAiroport")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
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
            ViewData["ArrivalAiroport"] = new SelectList(_context.Airports, "Id", "Name", flight.ArrivalAiroport);
            ViewData["DepartureAiroport"] = new SelectList(_context.Airports, "Id", "Name", flight.DepartureAiroport);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.ArrivalAiroportNavigation)
                .Include(f => f.DepartureAiroportNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }
    }
}
