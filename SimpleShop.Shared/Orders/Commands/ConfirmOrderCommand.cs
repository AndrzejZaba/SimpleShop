using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Shared.Orders.Commands
{
    public class ConfirmOrderCommand : IRequest
    {
        [Required]
        public string SessionId { get; set; }
    }
}
