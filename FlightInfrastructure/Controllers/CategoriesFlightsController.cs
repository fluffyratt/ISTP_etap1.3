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
    public class CategoriesFlightsController : Controller
    {
        private readonly DbflightsContext _context;

        public CategoriesFlightsController(DbflightsContext context)
        {
            _context = context;
        }

        // GET: CategoriesFlights
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if(id == null) return RedirectToAction("Categories", "Index");
            // знаходження авіарейсів за категорією
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = name;
            var flightByCategory = _context.CategoriesFlights.Where(b  => b.CategoryId == id).Include(b => b.Category);

            return View(flightByCategory.ToListAsync());

            var dbflightsContext = _context.CategoriesFlights.Include(c => c.Category).Include(c => c.Flight);
            return View(await dbflightsContext.ToListAsync());
        }

        // GET: CategoriesFlights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriesFlight = await _context.CategoriesFlights
                .Include(c => c.Category)
                .Include(c => c.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriesFlight == null)
            {
                return NotFound();
            }

            return View(categoriesFlight);
        }

        // GET: CategoriesFlights/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Description");
            return View();
        }

        // POST: CategoriesFlights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,FlightId,SeatsNumber,Price,Id")] CategoriesFlight categoriesFlight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriesFlight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoriesFlight.CategoryId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Description", categoriesFlight.FlightId);
            return View(categoriesFlight);
        }

        // GET: CategoriesFlights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriesFlight = await _context.CategoriesFlights.FindAsync(id);
            if (categoriesFlight == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoriesFlight.CategoryId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Description", categoriesFlight.FlightId);
            return View(categoriesFlight);
        }

        // POST: CategoriesFlights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,FlightId,SeatsNumber,Price,Id")] CategoriesFlight categoriesFlight)
        {
            if (id != categoriesFlight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriesFlight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesFlightExists(categoriesFlight.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoriesFlight.CategoryId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Description", categoriesFlight.FlightId);
            return View(categoriesFlight);
        }

        // GET: CategoriesFlights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriesFlight = await _context.CategoriesFlights
                .Include(c => c.Category)
                .Include(c => c.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriesFlight == null)
            {
                return NotFound();
            }

            return View(categoriesFlight);
        }

        // POST: CategoriesFlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriesFlight = await _context.CategoriesFlights.FindAsync(id);
            if (categoriesFlight != null)
            {
                _context.CategoriesFlights.Remove(categoriesFlight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesFlightExists(int id)
        {
            return _context.CategoriesFlights.Any(e => e.Id == id);
        }
    }
}
