using System;
using MediaCatalog.Core;
using MediaCatalog.Core.Interfaces;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class ProductManagerTests
    {
        /*
        private IProductManager productManager;

        public ProductManagerTests(){
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeCatalogRepository(start, end);
            IRepository<Room> roomRepository = new FakeProductRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            // Arrange
            DateTime date = DateTime.Today;

            // Act
            Action act = () => bookingManager.FindAvailableRoom(date, date);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
        }
        */
    }
}
