using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcmeWebAPI.Models
{
   
    public class AvailableFlightModel : FlightModel
    {
        public DateTime Date { get; set; }
        public int AvailableSeats { get; set; }  
    }

    public class AvailableFlightModelComparer : IEqualityComparer<AvailableFlightModel>
    {

        public bool Equals(AvailableFlightModel x, AvailableFlightModel y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.AvailableSeats == y.AvailableSeats
                && x.Date==y.Date
                && new FlightModelComparer().Equals(x, y);
        }

        public int GetHashCode(AvailableFlightModel obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashDate = obj.Date == null ? 0 : obj.Date.GetHashCode();
            int hashAvailableSeats = obj.AvailableSeats== null ? 0 : obj.AvailableSeats.GetHashCode();


            //Calculate the hash code for the product.
            return hashDate ^ hashAvailableSeats ^ new AvailableFlightModelComparer().GetHashCode(obj);
        }
    }
}