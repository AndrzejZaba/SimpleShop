using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Shared.Orders.Commands
{
    public class AddOrderCommand : IRequest
    {
        public decimal Value { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Pole 'E-mail' jest wymagane.")]
        [EmailAddress(ErrorMessage = "Wpisz prawidłowy adres E-mail.")]
        public string UserEmail { get; set; }
        public bool IsPaid { get; set; }
        public string SessionId { get; set; }
    }
}
