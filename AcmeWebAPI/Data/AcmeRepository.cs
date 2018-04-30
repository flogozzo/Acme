using System.Collections.Generic;
using AcmeWebAPI.Data.Entities;
using System;
using System.Linq;

namespace AcmeWebAPI.Data
{
    public class AcmeRepository:IAcmeRepository
    {
        private static IEnumerable<Flight> flights = new List<Flight> {
            new Flight { FlightNo=1, ArrivalCity="CityB", DeparterCity="CityA", StartTime=new TimeSpan(07, 00, 00), EndTime=new TimeSpan(08, 00, 00), PassengerCapacity=5,
                Bookings = new List<Booking>{
                            new Booking  { BookingNo=1, Date= new DateTime(2018, 3,2), FlightNo=1, PassengerName="Jim Brown" },
                            new Booking  {BookingNo=2, Date= new DateTime(2018, 3,2), FlightNo=1, PassengerName="Max Haler" },
                            new Booking  {BookingNo=3, Date= new DateTime(2018, 3, 3), FlightNo=1, PassengerName="Mary Street" }
                }
            },
            new Flight { FlightNo=2, ArrivalCity="CityC", DeparterCity="CityA", StartTime=new TimeSpan(07, 30, 00), EndTime=new TimeSpan(09, 30, 00), PassengerCapacity=3,
                Bookings = new List<Booking>{
                            new Booking  {BookingNo=4, Date= new DateTime(2018, 3,5), FlightNo=2, PassengerName="Karen Lom" },
                            new Booking  {BookingNo=5, Date= new DateTime(2018, 3,2), FlightNo=2, PassengerName="Mike Rom" },
                            new Booking  {BookingNo=6, Date= new DateTime(2018, 3,2), FlightNo=2, PassengerName="James B" },
                            new Booking  {BookingNo=7, Date= new DateTime(2018, 3,2), FlightNo=2, PassengerName="Linda A" },
                }
            },

            new Flight { FlightNo=3, ArrivalCity="CityA", DeparterCity="CityB", StartTime=new TimeSpan(08, 30, 00), EndTime=new TimeSpan(09, 30, 00), PassengerCapacity=5, 
                            Bookings = new List<Booking>{ new Booking  {BookingNo=8, Date= new DateTime(2018, 3,5), FlightNo=3, PassengerName="Max Haler" }
                            }
            },
            new Flight { FlightNo=4, ArrivalCity="CityA", DeparterCity="CityC", StartTime=new TimeSpan(10, 00, 00), EndTime=new TimeSpan(12, 00, 00), PassengerCapacity=3, Bookings=new List<Booking>{ } },
            
            new Flight { FlightNo=5, ArrivalCity="CityB", DeparterCity="CityA", StartTime=new TimeSpan(10, 30, 00), EndTime=new TimeSpan(11, 30, 00), PassengerCapacity=5, Bookings=new List<Booking>{ } },
            new Flight { FlightNo=6, ArrivalCity="CityC", DeparterCity="CityA", StartTime=new TimeSpan(12, 30, 00), EndTime=new TimeSpan(14, 30, 00), PassengerCapacity=3, Bookings =new List<Booking>{ }
            },
            new Flight { FlightNo=7, ArrivalCity="CityA", DeparterCity="CityB", StartTime=new TimeSpan(12, 00, 00), EndTime=new TimeSpan(13, 00, 00), PassengerCapacity=5, Bookings=new List<Booking>{ } },
            new Flight { FlightNo=8, ArrivalCity="CityA", DeparterCity="CityC", StartTime=new TimeSpan(15, 00, 00), EndTime=new TimeSpan(17, 00, 00), PassengerCapacity=3,
                Bookings =new List<Booking>{ new Booking  {BookingNo=9, Date= new DateTime(2018, 3,2), FlightNo=8, PassengerName="Linda A" }
                }
            },

        };

        private static int nextBookingNo=10;

        public IEnumerable<Flight> GetAllFlights()
        {
           
            return flights;
        }

        public Flight GetFlight(int id)
        {
            return flights.Where(f => f.FlightNo == id).FirstOrDefault();
        }

        
        public IEnumerable<Booking> GetBookings(string passengerName = "", DateTime? date = null, string arrivalCity = "", string departureCity = "", int flightNumber = -1)
        {
            return flights
                       .Where(f => f.FlightNo == (flightNumber == -1 ? f.FlightNo:flightNumber) 
                                    && f.ArrivalCity== (string.IsNullOrEmpty(arrivalCity)?f.ArrivalCity: arrivalCity) 
                                    && f.DeparterCity== (string.IsNullOrEmpty(departureCity)?f.DeparterCity:departureCity))
                       .SelectMany(f => f.Bookings.Where(b => b.PassengerName == (string.IsNullOrEmpty(passengerName) ? b.PassengerName : passengerName) && b.Date == (date == null ? b.Date : date)),
                                                            (f, b) => new Booking {BookingNo= b.BookingNo, PassengerName = b.PassengerName, FlightNo = b.FlightNo, Date = b.Date });
        }          

        public IEnumerable<AvailableFlight> GetAvailableFlights(DateTime startDate, DateTime endDate, int availSeats)
        {
            
            IEnumerable<Flight> fl= GetAllFlights();
            IEnumerable<AvailableFlight> result = new List<AvailableFlight>();
            for (DateTime d = startDate.Date; d <= endDate; d = d.AddDays(1))
            {
                DateTime x = d;
                var query =
                    from f in fl
                    join b in fl.SelectMany(f => f.Bookings).Where(b => b.Date == d)
                        on f.FlightNo equals b.FlightNo
                    into fb
                    where f.PassengerCapacity - fb.Count() > availSeats
                    select new AvailableFlight {Date=d, AvailableSeats= f.PassengerCapacity - fb.Count(), FlightNo =f.FlightNo, StartTime=f.StartTime, EndTime=f.EndTime, DeparterCity=f.DeparterCity, ArrivalCity=f.ArrivalCity, PassengerCapacity=f.PassengerCapacity };
                result= result.Concat(query.ToList());
            }

            return result.OrderBy(f=>f.Date);
        }
        public bool CreateBooking(Booking booking)
        {
            try
            {
                var bookings = flights.FirstOrDefault(f => f.FlightNo == booking.FlightNo).Bookings;
                booking.BookingNo = nextBookingNo++;
                bookings.Add(booking);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Save()
        {
            return true;
        }
    }
}