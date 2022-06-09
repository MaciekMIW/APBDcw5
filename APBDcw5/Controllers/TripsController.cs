using APBDcw5.Models.DTO;
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
        //Przygotuj końcówkę pozwalającą na usunięcie danych klienta
        //Końcówka przyjmująca dane wysłane na adres HTTP DELETE na adres /api/clients/{idClient}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveTrip(int id)
        {
            int result = await _dbService.removeTrip(id);
            return result == 0 ? Ok() : BadRequest("Can't remove trip, either it doesn't exist or it is assigned to a client!");
        }
        //Przygotuj końcówkę pozwalającą na przypisanie klienta do wycieczki
        //Końcówka powinna przyjmować żądania http POST wysłane na adres /api/trips/{idTrip}/clients
        [HttpPost]
        [Route("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(ClientToTrip clientToTrip)
        {
            var result = await _dbService.assignClientToTrip(clientToTrip);
            if(result == -1)
            {
                return BadRequest("Customer is already assigned to this trip!");
            }
            if(result == -2)
            {
                return BadRequest("Trip does not exist!");
            }
            return Ok();
        }
    }
}
