using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcmeWebAPI.Models
{
    public class BookingModel
    {
        public int BookingNo { get; set; }
        public string PassengerName { get; set; }
        public DateTime Date { get; set; }
        public int FlightNo { get; set; }
    }

    public class BookingModelComparer : IEqualityComparer<BookingModel>
    {

        public bool Equals(BookingModel x, BookingModel y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.BookingNo == y.BookingNo
                && x.Date == y.Date
                && x.FlightNo == y.FlightNo
                && x.PassengerName == y.PassengerName;
        }

        public int GetHashCode(BookingModel obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashBookinNo = obj.BookingNo == null ? 0 : obj.BookingNo.GetHashCode();
            int hashDate = obj.Date == null ? 0 : obj.Date.GetHashCode();
            int hashFlightNo = obj.FlightNo == null ? 0 : obj.FlightNo.GetHashCode();
            int hashPassengerNme = obj.PassengerName == null ? 0 : obj.PassengerName.GetHashCode();


            //Calculate the hash code for the product.
            return hashBookinNo ^ hashDate^ hashFlightNo ^ hashPassengerNme;
        }
    }
}