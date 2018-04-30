using AcmeWebAPI.Data;
using AcmeWebAPI.Data.Entities;
using AcmeWebAPI.Models;
using AcmeWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AcmeWebAPI.Controllers
{
    public class BookingsController : BaseApiController
    {
        public BookingsController(IAcmeRepository repo, ILogger logger)
         : base(repo, logger)
        {
        }
        public IEnumerable<BookingModel> Get(string passengerName = "", DateTime? date = null, string arrivalCity = "", string departerCity = "", int flightNumber = -1)
        {
           
            return AcmeRepo.GetBookings(passengerName,date,arrivalCity,departerCity,flightNumber).ToList().Select(b => AcmeModelFactory.Create(b));
        }
        public HttpResponseMessage Post([FromBody] BookingModel bm)
        {
            try
            {
                Booking booking = AcmeEntityFactory.Create(bm);

                //validate the flight number.
                if (AcmeRepo.GetFlight(booking.FlightNo) == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid flight number in booking.");

                //make sure the passenger is not already booked for this flight on this date.
                if(AcmeRepo.GetBookings(booking.PassengerName, booking.Date, "", "", booking.FlightNo).FirstOrDefault() != null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Booking already exists for this passenger for flight {0} on date {1}", booking.FlightNo,booking.Date));
                //check available seats
                if( AcmeRepo.GetAvailableFlights(booking.Date, booking.Date, 1).FirstOrDefault(f=>f.FlightNo==booking.FlightNo) ==null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("No available seats for flight {0} on date {1}", booking.FlightNo, booking.Date));

                if (AcmeRepo.CreateBooking(booking) && AcmeRepo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, AcmeModelFactory.Create(booking));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save booking.");
                }               
            }
            catch (Exception ex)
            {
                _logger.LogMessage("Exception: " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
