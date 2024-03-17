using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Search.Application.Commands;
using Search.Application.Handlers;
using Search.Domain.Dto;
using Search.Domain.Interfaces;

namespace Search.Test
{
    [TestClass]
    public class CreateBookingCommandHandlerTests
    {
        private readonly Mock<IBookingRepository> _repositoryMock;
        private readonly CreateBookingCommandHandler createBookingCommandHandler;
        public CreateBookingCommandHandlerTests()
        {
            _repositoryMock = new Mock<IBookingRepository>();
            createBookingCommandHandler = new CreateBookingCommandHandler(_repositoryMock.Object);
        }

        [TestMethod]
        public void CreateBookingCommandHandler_WhenCalled_Success()
        {
            //Arrange
            var bookings = new List<BookingDetail>()
            {
                new BookingDetail() { BookingDate = DateTime.UtcNow, FlightId = 1, UserId = 1, SeatNo = "1B", Status = "Booked"},
                new BookingDetail() { BookingDate = DateTime.UtcNow, FlightId = 2, UserId = 2, SeatNo = "1B", Status = "Booked"}
            };

            _repositoryMock.Setup(x => x.Create(It.IsAny<List<BookingDetail>>())).ReturnsAsync(bookings);
            var createBookingCommand = new CreateBookingCommandRequest();

            //Act
            var result = createBookingCommandHandler.Handle(createBookingCommand, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}