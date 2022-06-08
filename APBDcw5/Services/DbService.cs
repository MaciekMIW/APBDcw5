using APBDcw5.Models;
using APBDcw5.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw5.Services
{
    public class DbService : IDbService
    {
        private readonly _2019SBDContext _dbContext;
        public DbService(_2019SBDContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<SomeTrip>> GetTrips()
        {
            return await _dbContext.Trips
                .Select(e => new SomeTrip
                {
                    Name = e.Name,
                    Description = e.Description,
                    MaxPeople = e.MaxPeople,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    Countries = e.CountryTrips.Select(e => new SomeCountry { Name = e.IdCountryNavigation.Name }).ToList(),
                    Clients = e.ClientTrips.Select(e=> new SomeClient { FirstName = e.IdClientNavigation.FirstName, LastName=e.IdClientNavigation.LastName})
                }).ToListAsync();
        }
    }
}
