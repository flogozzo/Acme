using AcmeWebAPI.Data;
using AcmeWebAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcmeWebAPI.Models
{
    public class ModelFactory
    {
        private IAcmeRepository _repo;
        public ModelFactory(IAcmeRepository repo)
        {
           
            _repo = repo;
        }
        public FlightModel Create(Flight flight)
        {
            return new FlightModel()
            {
                ArrivalCity = flight.ArrivalCity,
                DeparterCity = flight.DeparterCity,
                StartTime = flight.StartTime,
                EndTime = flight.EndTime,
                FlightNo = flight.FlightNo,
                PassengerCapacity = flight.PassengerCapacity,
                //Bookings = flight.Bookings.Select(b => Create(b))
            };
        }
        public BookingModel Create(Booking booking)
        {
            return new BookingModel()
            {
                BookingNo=booking.BookingNo, 
                Date = booking.Date,
                PassengerName = booking.PassengerName,
                FlightNo=booking.FlightNo
            };
        }

        
        public AvailableFlightModel Create(AvailableFlight flight)
        {
            return new AvailableFlightModel()
            {
                ArrivalCity = flight.ArrivalCity,
                DeparterCity = flight.DeparterCity,
                StartTime = flight.StartTime,
                EndTime = flight.EndTime,
                FlightNo = flight.FlightNo,
                PassengerCapacity = flight.PassengerCapacity,
                Date = flight.Date,
                AvailableSeats = flight.AvailableSeats
            };
        }
    }
}