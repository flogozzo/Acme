using System;

namespace AcmeWebAPI.Data.Entities
{
    public class Booking
    {

        public int BookingNo { get; set; }
        public string PassengerName { get; set; }
        public DateTime Date { get; set; }
        public int FlightNo { get; set; }
    }
}