using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Shared.Orders.Commands;

namespace SimpleShop.Application.Orders.Commands
{
    internal class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand>
    {
        private readonly IApplicationDbContext _context;
		private readonly IPaymentService _paymentService;

		public ConfirmOrderCommandHandler(
            IApplicationDbContext context,
            IPaymentService paymentService)
        {
            _context = context;
			_paymentService = paymentService;
		}

        public async Task Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            if (!_paymentService.IsPaid(request.SessionId))
                throw new Exception("Payment pending...");

            var orderToConfirm = await _context
                .Orders
                .FirstOrDefaultAsync(x => x.SessionId == request.SessionId);

            if (orderToConfirm == null)
                return;

            orderToConfirm.IsPaid = true;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
