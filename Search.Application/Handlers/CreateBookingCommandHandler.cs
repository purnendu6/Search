using MediatR;
using Search.Application.Commands;
using Search.Domain.Entities;
using Search.Domain.Interfaces;

namespace Search.Application.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IBookingRepository _bookingRepository;
        public CreateBookingCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public Task<int> Handle(CreateBookingCommand createBookingCommand, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                BookingDetails = createBookingCommand.BookingDetails
            };
            return _bookingRepository.Create(booking);
        }
    }
}
