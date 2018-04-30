using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using AcmeWebAPI.Controllers;
using AcmeWebAPI.Data.Entities;
using AcmeWebAPI.Data;
using AcmeWebAPI.Models;
using AcmeWebAPI.Services;
using Moq;
using System.Net.Http;
using System.Web.Http;

namespace AcmeWebAPI.Tests.Controllers
{
    [TestClass]
    public class BookingsControllerTest
    {


        [TestMethod]
        public void GetBookingByPassenger()
        {
            List<BookingModel> expectedResult = new List<BookingModel>
            {
                new BookingModel { BookingNo = 2, Date = new DateTime(2018, 3, 2), FlightNo = 1, PassengerName = "Max Haler" },
                new BookingModel  {BookingNo= 8, Date= new DateTime(2018, 3,5), FlightNo=3, PassengerName="Max Haler" }
            };

            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            BookingsController controller = new BookingsController(repo, mockLogger.Object);

            // Act
            var result = controller.Get("Max Haler");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.SequenceEqual(expectedResult, new BookingModelComparer()), "Comparison of bookings failed");


        }

        [TestMethod]
        public void GetBookingByPassengerAndDate()
        {
            List<BookingModel> expectedResult = new List<BookingModel>
            {
                new BookingModel { BookingNo = 2, Date = new DateTime(2018, 3, 2), FlightNo = 1, PassengerName = "Max Haler" },
            };

            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            BookingsController controller = new BookingsController(repo, mockLogger.Object);

            // Act
            var result = controller.Get("Max Haler", new DateTime(2018, 3, 2));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.SequenceEqual(expectedResult, new BookingModelComparer()), "Comparison of bookings failed");


        }

        [TestMethod]
        public void GetBookingByPassengerAndDateAndArrivalCity()
        {
            List<BookingModel> expectedResult = new List<BookingModel>
            {
                new BookingModel  {BookingNo=9, Date= new DateTime(2018, 3,2), FlightNo=8, PassengerName="Linda A" }
            };

            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            BookingsController controller = new BookingsController(repo, mockLogger.Object);

            // Act
            var result = controller.Get("Linda A", new DateTime(2018, 3, 2), "CityA");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.SequenceEqual(expectedResult, new BookingModelComparer()), "Comparison of bookings failed");


        }

        [TestMethod]
        public void GetBookingByPassengerAndFlightNo()
        {
            List<BookingModel> expectedResult = new List<BookingModel>
            {
                new BookingModel  {BookingNo=7, Date= new DateTime(2018, 3,2), FlightNo=2, PassengerName="Linda A" }
            };
            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            BookingsController controller = new BookingsController(repo, mockLogger.Object);

            // Act
            var result = controller.Get("Linda A", null, "","", 2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.SequenceEqual(expectedResult, new BookingModelComparer()), "Comparison of bookings failed");
        }

        [TestMethod]
        public void GetBookingByFlightNo()
        {
            List<BookingModel> expectedResult = new List<BookingModel>
            {
                    new BookingModel  {BookingNo=4, Date= new DateTime(2018, 3,5), FlightNo=2, PassengerName="Karen Lom" },
                    new BookingModel  {BookingNo=5, Date= new DateTime(2018, 3,2), FlightNo=2, PassengerName="Mike Rom" },
                    new BookingModel  {BookingNo=6, Date= new DateTime(2018, 3,2), FlightNo=2, PassengerName="James B" },
                    new BookingModel  {BookingNo=7, Date= new DateTime(2018, 3,2), FlightNo=2, PassengerName="Linda A" }
            };

            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            BookingsController controller = new BookingsController(repo, mockLogger.Object);

            // Act
            var result = controller.Get("", null, "", "", 2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
            Assert.IsTrue(result.SequenceEqual(expectedResult, new BookingModelComparer()), "Comparison of bookings failed");
        }

        [TestMethod]
        public void CreateValidBooking()
        {
           
            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            BookingsController controller = new BookingsController(repo, mockLogger.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            BookingModel bm = new BookingModel
            {
                PassengerName = "James B",
                Date = new DateTime(2018, 3, 7),
                FlightNo = 5
            };

            // Act
            var result = controller.Post(bm);

            // Assert
            Assert.IsNotNull(result);
            BookingModel newBookingModel;
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.Created);
            Assert.IsTrue(result.TryGetContentValue<BookingModel>(out newBookingModel));
            Assert.AreEqual("James B", newBookingModel.PassengerName);
            Assert.AreEqual(5, newBookingModel.FlightNo);
            Assert.AreEqual(10, newBookingModel.BookingNo);

        }
        [TestMethod]
        public void CreateInValidBookingFlightNo()
        {

            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            BookingsController controller = new BookingsController(repo, mockLogger.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            BookingModel bm = new BookingModel
            {
                PassengerName = "John SMith",
                Date = new DateTime(2018, 3, 7),
                FlightNo = 53
            };

            // Act
            var result = controller.Post(bm);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
            HttpError err;
            Assert.IsTrue(result.TryGetContentValue <HttpError>(out err));
            Assert.IsTrue(err.Message== "Invalid flight number in booking.");

        }

        [TestMethod]
        public void CreateInValidBookingNoSeatsAvailable()
        {

            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            BookingsController controller = new BookingsController(repo, mockLogger.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            BookingModel bm = new BookingModel
            {
                PassengerName = "John SMith",
                Date = new DateTime(2018, 3, 2),
                FlightNo = 2
            };
 
            // Act
            var result = controller.Post(bm);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
            HttpError err;
            Assert.IsTrue(result.TryGetContentValue<HttpError>(out err));
            Assert.IsTrue(err.Message.IndexOf("No available seats for flight 2 on date")!=-1);

        }


        [TestMethod]
        public void CreateInValidBookingAlreadyBooked()
        {

            // Arrange
            IAcmeRepository repo = new AcmeRepository();
            var mockLogger = new Mock<ILogger>();

            BookingsController controller = new BookingsController(repo, mockLogger.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            BookingModel bm = new BookingModel
            {
                PassengerName = "Karen Lom",
                Date = new DateTime(2018, 3, 5),
                FlightNo = 2
            };

            // Act
            var result = controller.Post(bm);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
            HttpError err;
            Assert.IsTrue(result.TryGetContentValue<HttpError>(out err));
            Assert.IsTrue(err.Message.IndexOf("Booking already exists for this passenger for flight 2 on date") != -1);

        }

     

    }
}
