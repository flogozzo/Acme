using AcmeWebAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeWebAPI.Data
{
    public interface IAcmeRepository
    {
        IEnumerable<Flight> GetAllFlights();

        Flight GetFlight(int id);

        IEnumerable<Booking> GetBookings(string passengerName, DateTime? date, string arrivalCity, string departureCity, int flightNumber);

        IEnumerable<AvailableFlight> GetAvailableFlights(DateTime startDate, DateTime endDate, int availSeats);

        bool CreateBooking(Booking booking);

        bool Save();
    }
}
