using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Search.Application.Commands;
using Search.Application.Handlers;
using Search.Domain.Entities;
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
            _repositoryMock.Setup(x => x.Create(It.IsAny<Booking>())).ReturnsAsync(1);
            var createBookingCommand = new CreateBookingCommand
            {
                BookingDetails = "description"
            };

            //Act
            var result = createBookingCommandHandler.Handle(createBookingCommand, CancellationToken.None).Result;

            //Assert
            Assert.AreEqual(1, result);
        }
    }
}