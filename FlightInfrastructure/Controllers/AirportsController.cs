using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightDomain.Model;
using FlightInfrastructure;

namespace FlightInfrastructure.Controllers
{
    public class AirportsController : Controller
    {
        private readonly DbflightsContext _context;

        public AirportsController(DbflightsContext context)
        {
            _context = context;
        }

        // GET: Airports
        public async Task<IActionResult> Index()
        {
            var dbflightsContext = _context.Airports.Include(a => a.City);
            return View(await dbflightsContext.ToListAsync());
        }

        public async Task<IActionResult> ArrivalAirport(int? id)
        {
            var dbflightsContext = await _context.Airports.Include(f => f.FlightArrivalAiroportNavigations).Where(a => a.Id == id).FirstOrDefaultAsync();
            return View(dbflightsContext.FlightArrivalAiroportNavigations);
        }

        public async Task<IActionResult> DepartureAirport(int? id)
        {
            var dbflightsContext = await _context.Airports.Include(f => f.FlightDepartureAiroportNavigations).Where(b => b.Id == id).FirstOrDefaultAsync();
            return View(dbflightsContext.FlightDepartureAiroportNavigations);
        }





        // GET: Airports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports
                .Include(a => a.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {
            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        // POST: Airports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, [Bind("Id, City, Name")] Airport airport)
        {

            var existingAirport = await _context.Airports.FirstOrDefaultAsync(c => c.Name == airport.Name);
            if (existingAirport != null)
            {
                ModelState.AddModelError("Name", "Аеоропорт з такою назвою вже існує.");
                return View(airport);
            }

            City city = _context.Cities.Include(c => c.Country).FirstOrDefault(c => c.Name == airport.City.Name);
            airport.City = city;
            ModelState.Clear();
            TryValidateModel(airport);


            if (ModelState.IsValid)
            {
                _context.Add(airport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "Name", airport.City.Name);
            return View(airport);
        }

        // GET: Airports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", airport.CityId);
            return View(airport);
        }

        // POST: Airports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityId,Name")] Airport airport)
        {
            if (id != airport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportExists(airport.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", airport.CityId);
            return View(airport);
        }

        // GET: Airports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports
                .Include(a => a.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: Airports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airport = await _context.Airports.FindAsync(id);
            if (airport != null)
            {
                _context.Airports.Remove(airport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirportExists(int id)
        {
            return _context.Airports.Any(e => e.Id == id);
        }
    }
}
