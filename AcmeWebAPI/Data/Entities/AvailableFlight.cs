using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcmeWebAPI.Data.Entities
{
    public class AvailableFlight:Flight
    {
        public DateTime Date { get; set; }
        public int AvailableSeats { get; set; }
    }
}