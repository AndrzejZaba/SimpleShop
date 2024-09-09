using MediatR;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Orders.Commands;


namespace SimpleShop.Application.Orders.Commands
{
    internal class AddOrderCommandHandler : IRequestHandler<AddOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public AddOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                IsPaid = false,
                SessionId = request.SessionId,
                UserId = request.UserId,
                Value = request.Value
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
