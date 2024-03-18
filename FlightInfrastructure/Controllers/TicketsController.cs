using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightDomain.Model;
using FlightInfrastructure;
using Microsoft.Identity.Client;

namespace FlightInfrastructure.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DbflightsContext _context;

        public TicketsController(DbflightsContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var dbflightsContext = _context.Tickets.Include(t => t.CategoriesFlights).Include(t => t.User).Include(t => t.CategoriesFlights.Category);
            return View(await dbflightsContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.CategoriesFlights)
                .Include(t => t.User)
                .Include(t => t.CategoriesFlights.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            IList<CategoryFlightsSelector> list = new List<CategoryFlightsSelector>();
            foreach (var flight in _context.CategoriesFlights.Include(c => c.Category).AsNoTracking())
           {
              list.Add(new CategoryFlightsSelector { Id = flight.Id, Name = flight.Category.Name });
           } 
            ViewData["CategoriesFlightsId"] = new SelectList(_context.CategoriesFlights, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        private class CategoryFlightsSelector  {
            public string Name { get; set; } = null!;
            public int Id { get; set; }
           
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PurchaseDate,UserId,CategoriesFlightsId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriesFlightsId"] = new SelectList(_context.CategoriesFlights, "Id", "Id", ticket.CategoriesFlightsId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", ticket.UserId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["CategoriesFlightsId"] = new SelectList(_context.CategoriesFlights, "Id", "Id", ticket.CategoriesFlightsId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", ticket.UserId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PurchaseDate,UserId,CategoriesFlightsId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            ViewData["CategoriesFlightsId"] = new SelectList(_context.CategoriesFlights, "Id", "Id", ticket.CategoriesFlightsId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", ticket.UserId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.CategoriesFlights)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
