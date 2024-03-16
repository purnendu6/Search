using MediatR;

namespace Search.Application.Commands
{
    public class CreateBookingCommand : IRequest<int>
    {
        public string BookingDetails { get; set; } = string.Empty;
    }
}
