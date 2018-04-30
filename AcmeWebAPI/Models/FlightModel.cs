using AcmeWebAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcmeWebAPI.Models
{
    public class FlightModel
    {
        public int FlightNo { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int PassengerCapacity { get; set; }
        public string DeparterCity { get; set; }
        public string ArrivalCity { get; set; }
        //public IEnumerable<BookingModel> Bookings { get; set; }

       
    }

    public class FlightModelComparer : IEqualityComparer<FlightModel>
    {

        public bool Equals(FlightModel x, FlightModel y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.ArrivalCity == y.ArrivalCity
                && x.DeparterCity == y.DeparterCity
                && x.EndTime == y.EndTime
                && x.FlightNo == y.FlightNo
                && x.PassengerCapacity == y.PassengerCapacity
                && x.StartTime == y.StartTime;
        }

        public int GetHashCode(FlightModel obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashArrivalCity = obj.ArrivalCity == null ? 0 : obj.ArrivalCity.GetHashCode();
            int hashDeparterCity = obj.DeparterCity == null ? 0 : obj.DeparterCity.GetHashCode();
            int hashEndTime = obj.EndTime == null ? 0 : obj.EndTime.GetHashCode();
            int hashFlightNo = obj.FlightNo == null ? 0 : obj.FlightNo.GetHashCode();
            int hashPassengerCapacity = obj.PassengerCapacity == null ? 0 : obj.PassengerCapacity.GetHashCode();
            int hashStartTime = obj.StartTime == null ? 0 : obj.StartTime.GetHashCode();


            //Calculate the hash code for the product.
            return hashArrivalCity ^ hashDeparterCity ^ hashEndTime ^ hashFlightNo ^ hashPassengerCapacity ^ hashStartTime;
        }
    }
}