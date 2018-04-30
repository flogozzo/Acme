using AcmeWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcmeWebAPI.Data.Entities
{
    public class EntityFactory
    {
        public Booking Create(BookingModel bm)
        {

            try
            {
                var booking = new Booking()
                {
                    BookingNo = bm.BookingNo,
                    Date = bm.Date,
                    FlightNo = bm.FlightNo,
                    PassengerName = bm.PassengerName
                };



                return booking;
            }
            catch
            {
                return null;
            }
        }
    }
}