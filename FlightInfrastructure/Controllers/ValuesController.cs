using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using FlightInfrastructure.Models;

namespace FlightInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketChartsController : ControllerBase
    {
        private record CountByCategoryResponseItem(string CategoryName, int Count);

        private readonly DbflightsContext _context;

        public TicketChartsController(DbflightsContext context)
        {
            _context = context;
        }

        [HttpGet("countByCategory")]
        public async Task<JsonResult> GetCountByCategoryAsync(CancellationToken cancellationToken)
        {
            var responseItems = await _context
                .CategoriesFlights
                .Include(category => category.Tickets)
                .Select(category => new CountByCategoryResponseItem(category.Category.Name, category.Tickets.Count))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }
    }
}