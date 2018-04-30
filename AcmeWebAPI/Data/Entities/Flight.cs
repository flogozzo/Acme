using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcmeWebAPI.Data.Entities
{
    public class Flight
    {
        public int FlightNo { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int PassengerCapacity { get; set; }
        public string DeparterCity { get; set; }
        public string ArrivalCity { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}