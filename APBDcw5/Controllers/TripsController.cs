using APBDcw5.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw5.Controllers
{
    [Route("api/{controller}")]
    [ApiController]   
    public class TripsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public TripsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }
    }
}
