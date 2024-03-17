using MediatR;
using Search.Application.Commands;
using Search.Domain.Interfaces;

namespace Search.Application.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommandRequest>
    {
        private readonly IBookingRepository _bookingRepository;
        public CreateBookingCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public Task Handle(CreateBookingCommandRequest createBookingCommand, CancellationToken cancellationToken)
        {
            return _bookingRepository.Create(createBookingCommand.BookingDetails);
        }
    }
}
