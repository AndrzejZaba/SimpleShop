using SimpleShop.Shared.Payments.Commands;

namespace SimpleShop.Client.HttpRepository.Interfaces;

public interface IPaymentHttpRepository
{
    Task<string> Add(AddPaymentCommand command);
}
