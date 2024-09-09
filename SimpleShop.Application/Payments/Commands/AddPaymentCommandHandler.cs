using MediatR;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Shared.Payments.Commands;

namespace SimpleShop.Application.Payments.Commands
{
	internal class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, string>
    {
        private readonly IPaymentService _paymentService;

        public AddPaymentCommandHandler(
            IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<string> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            return _paymentService.Create(request.ClientUrl, request.Value);
        }
    }
}
