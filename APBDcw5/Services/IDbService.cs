using APBDcw5.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw5.Services
{
    public interface IDbService
    {
        Task<IEnumerable<SomeTrip>> GetTrips();
        Task<int> removeTrip(int id);
        Task<int> assignClientToTrip(ClientToTrip clientToTrip);
    }
}
