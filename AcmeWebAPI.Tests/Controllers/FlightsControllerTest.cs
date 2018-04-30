using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcmeWebAPI.Controllers;
using AcmeWebAPI.Data.Entities;
using AcmeWebAPI.Data;
using AcmeWebAPI.Models;
using AcmeWebAPI.Services;
using Moq;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace AcmeWebAPI.Tests.Controllers
{
    [TestClass]
    public class FlightsControllerTest
    {
        
        [TestMethod]
        public void GetAllFlights()
        {
            List<FlightModel> expectedResult = new List<FlightModel>
            {
                new FlightModel { FlightNo=1, ArrivalCity="CityB", DeparterCity="CityA", StartTime=new TimeSpan(07, 00, 00), EndTime=new TimeSpan(08, 00, 00), PassengerCapacity=5},
                new FlightModel { FlightNo=2, ArrivalCity="CityC", DeparterCity="CityA", StartTime=new TimeSpan(07, 30, 00), EndTime=new TimeSpan(09, 30, 00), PassengerCapacity=3 },

                new FlightModel { FlightNo=3, ArrivalCity="CityA", DeparterCity="CityB", StartTime=new TimeSpan(08, 30, 00), EndTime=new TimeSpan(09, 30, 00), PassengerCapacity=5},
                new FlightModel { FlightNo=4, ArrivalCity="CityA", DeparterCity="CityC", StartTime=new TimeSpan(10, 00, 00), EndTime=new TimeSpan(12, 00, 00), PassengerCapacity=3},

                new FlightModel { FlightNo=5, ArrivalCity="CityB", DeparterCity="CityA", StartTime=new TimeSpan(10, 30, 00), EndTime=new TimeSpan(11, 30, 00), PassengerCapacity=5 },
                new FlightModel { FlightNo=6, ArrivalCity="CityC", DeparterCity="CityA", StartTime=new TimeSpan(12, 30, 00), EndTime=new TimeSpan(14, 30, 00), PassengerCapacity=3 },
                new FlightModel { FlightNo=7, ArrivalCity="CityA", DeparterCity="CityB", StartTime=new TimeSpan(12, 00, 00), EndTime=new TimeSpan(13, 00, 00), PassengerCapacity=5 },
                new FlightModel { FlightNo=8, ArrivalCity="CityA", DeparterCity="CityC", StartTime=new TimeSpan(15, 00, 00), EndTime=new TimeSpan(17, 00, 00), PassengerCapacity=3 }            
            };

            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            FlightsController controller = new FlightsController(repo, mockLogger.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(8, result.Count());
            Assert.IsTrue(result.SequenceEqual(expectedResult, new FlightModelComparer()), "Comparison of flights failed");


        }
        [TestMethod]
        public void GetAvailableFlightsWithAtLeast1()
        {
            List<AvailableFlightModel> expectedResult = new List<AvailableFlightModel>
            {
                new AvailableFlightModel { FlightNo=1, ArrivalCity="CityB", DeparterCity="CityA", StartTime=new TimeSpan(07, 00, 00), EndTime=new TimeSpan(08, 00, 00), PassengerCapacity=5, AvailableSeats=3, Date=new DateTime(2018, 3, 2)},

                new AvailableFlightModel { FlightNo=3, ArrivalCity="CityA", DeparterCity="CityB", StartTime=new TimeSpan(08, 30, 00), EndTime=new TimeSpan(09, 30, 00), PassengerCapacity=5, AvailableSeats=5, Date=new DateTime(2018, 3, 2)},
                new AvailableFlightModel { FlightNo=4, ArrivalCity="CityA", DeparterCity="CityC", StartTime=new TimeSpan(10, 00, 00), EndTime=new TimeSpan(12, 00, 00), PassengerCapacity=3, AvailableSeats=3, Date=new DateTime(2018, 3, 2)},

                new AvailableFlightModel { FlightNo=5, ArrivalCity="CityB", DeparterCity="CityA", StartTime=new TimeSpan(10, 30, 00), EndTime=new TimeSpan(11, 30, 00), PassengerCapacity=5, AvailableSeats=5, Date=new DateTime(2018, 3, 2) },
                new AvailableFlightModel { FlightNo=6, ArrivalCity="CityC", DeparterCity="CityA", StartTime=new TimeSpan(12, 30, 00), EndTime=new TimeSpan(14, 30, 00), PassengerCapacity=3, AvailableSeats=3, Date=new DateTime(2018, 3, 2) },
                new AvailableFlightModel { FlightNo=7, ArrivalCity="CityA", DeparterCity="CityB", StartTime=new TimeSpan(12, 00, 00), EndTime=new TimeSpan(13, 00, 00), PassengerCapacity=5, AvailableSeats=5, Date=new DateTime(2018, 3, 2) },
                new AvailableFlightModel { FlightNo=8, ArrivalCity="CityA", DeparterCity="CityC", StartTime=new TimeSpan(15, 00, 00), EndTime=new TimeSpan(17, 00, 00), PassengerCapacity=3, AvailableSeats=2, Date=new DateTime(2018, 3, 2) }
            };

            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            FlightsController controller = new FlightsController(repo, mockLogger.Object);

            // Act
            var result = controller.Get(new DateTime(2018, 3, 2), new DateTime(2018, 3, 2), 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.IsTrue(result.SequenceEqual(expectedResult, new AvailableFlightModelComparer()), "Comparison of available flights failed");


        }

    }
}
