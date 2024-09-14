using SimpleShop.Shared.Orders.Commands;

namespace SimpleShop.Client.HttpRepository.Interfaces;

public interface IOrderHttpRepository
{
    Task Add(AddOrderCommand command);
    Task Confirm(ConfirmOrderCommand command);  
}
