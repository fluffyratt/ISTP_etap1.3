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
    public class ValuesController : ControllerBase
    {
        private readonly DbflightsContext _context;

        public ValuesController(DbflightsContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public IActionResult JsonData()
        {
            try
            {
                var users = _context.Users.Include(c => c.Tickets).ToList();
                var uTickets = new List<UserTicketsDto>();

                foreach (var user in users)
                {
                    uTickets.Add(new UserTicketsDto
                    {
                        User = user.Name + " " + user.Email,
                        TicketCount = user.Tickets.Count()
                    });
                }

                return Ok(uTickets);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

namespace FlightInfrastructure.Models
{
    public class UserTicketsDto
    {
        public string User { get; set; }
        public int TicketCount { get; set; }
    }
}


