using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
       private readonly DbflightsContext _context;

        public ChartController(DbflightsContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]

        public JsonResult JsonData()
        {
            var tickets = _context.CategoriesFlights.Include(c => c.Category).Include(c => c.Tickets).ToList();
            List<object> catFlight = new List<object>();
            catFlight.Add(new[] { "Категорія", "Квиток" });
            foreach (var c in tickets)
            {
                catFlight.Add(new object[] { c.Category.Name, c.Tickets.Count});

            }
            return new JsonResult(catFlight);
        }
    }
}


