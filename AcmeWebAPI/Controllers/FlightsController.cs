using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AcmeWebAPI.Data;
using AcmeWebAPI.Models;
using AcmeWebAPI.Services;

namespace AcmeWebAPI.Controllers
{
    public class FlightsController : BaseApiController
    {
        public FlightsController(IAcmeRepository repo, ILogger logger)
          : base(repo,logger)
        {
        }
        public IEnumerable<FlightModel> Get()
        {
            return AcmeRepo.GetAllFlights().ToList().Select(f => AcmeModelFactory.Create(f));
        }
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var result = AcmeRepo.GetFlight(id);

                if (result == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, AcmeModelFactory.Create(result));
            }
            catch(Exception ex)
            {
                _logger.LogMessage("Exception: " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        
        public IEnumerable<AvailableFlightModel> Get(DateTime startDate, DateTime endDate, int availSeats)
        {
            return AcmeRepo.GetAvailableFlights(startDate, endDate, availSeats).ToList().Select(f=>AcmeModelFactory.Create(f));
        }
    }
}
