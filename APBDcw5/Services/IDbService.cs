using APBDcw5.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw5.Services
{
    public interface IDbService
    {
        Task<IEnumerable<SomeTrip>> GetTrips();
    }
}
