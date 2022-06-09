using APBDcw5.Models;
using APBDcw5.Models.DTO;
using Microsoft.AspNetCore.Mvc;
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
                }).OrderBy(e=> e.DateFrom).ToListAsync();
        }

        public async Task<int> removeTrip(int id)
        {
            //Końcówka powinna najpierw sprawdzić czy klient nie posiada żadnych przypisanych wycieczek.
            if (_dbContext.ClientTrips.Where(e => e.IdClient == id) != null)
            {
                //Jeśli klient posiada co najmniej jedną przypisaną wycieczkę – zwracamy błąd i usunięcie nie dochodzi do skutku
                return -1;
            }
            var trip = new Trip() { IdTrip = id };
            _dbContext.Attach(trip);
            _dbContext.Remove(trip);
            await _dbContext.SaveChangesAsync();
            return 0;
        }
        public async Task<int> assignClientToTrip(ClientToTrip clientToTrip)
        {
            //Po przyjęciu danych sprawdzamy:
            //Czy klient o danym numerze PESEL istnieje. Jeśli nie, dodajemy go do bazy danych.
            Client client = _dbContext.Clients.Where(e => e.Pesel.Equals(clientToTrip.Pesel)).FirstOrDefault();
            if (client == null)
            {
                client = await addClient(clientToTrip);
            }
            //Czy klient nie jest już zapisaną na wspomnianą wycieczkę – w takim wypadku zwracamy błąd
            if (_dbContext.ClientTrips.Where(e=> e.IdClientNavigation.IdClient==client.IdClient && e.IdTripNavigation.IdTrip==clientToTrip.IdTrip).Any() == true)
            {
                return -1;
            }
            //Czy wycieczka istnieje – jeśli nie – zwracamy błąd
            Trip trip = _dbContext.Trips.Where(e => e.IdTrip == clientToTrip.IdTrip).FirstOrDefault();
            if(trip == null)
            {
                return -2;
            }
            //Dodajemy wycieczkę
            //Console.WriteLine("Client to trip payment date: CCCCCCCCC  "+clientToTrip.PaymentDate);
            ClientTrip clientTrip = new ClientTrip()
            {
                IdClient = client.IdClient,
                IdTrip = trip.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientToTrip.PaymentDate
            };
            _dbContext.Add(clientTrip);
            await _dbContext.SaveChangesAsync();
            return 0;
        }

        private async Task<Client> addClient(ClientToTrip clientToTrip)
        {
            Client client = new Client()
            {
                FirstName = clientToTrip.FirstName,
                LastName = clientToTrip.LastName,
                Email = clientToTrip.Email,
                Telephone = clientToTrip.Telephone,
                Pesel = clientToTrip.Pesel
            };
            _dbContext.Add(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }
    }
}
