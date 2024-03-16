using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Search.Application.Handlers;
using Search.Application.Queries;
using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Test
{
    [TestClass]
    public class GetBookingListQueryHandlerTests
    {
        private readonly Mock<IBookingRepository> _repositoryMock;
        private readonly GetBookingListQueryHandler getBookingListQueryHandler;
        public GetBookingListQueryHandlerTests()
        {
            _repositoryMock = new Mock<IBookingRepository>();
            getBookingListQueryHandler = new GetBookingListQueryHandler(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task GetBookingListQueryHandler_WhenCalled_Success()
        {
            // Arrange
            var bookingList = new List<Booking>()
            {
                new Booking() {BookingDetails = "test1"},
                new Booking() {BookingDetails = "test2"}
            };
            _repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(bookingList);
            var query = new GetBookingListQuery();

            // Act
            var result = await getBookingListQueryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}
