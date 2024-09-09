using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Shared.Payments.Commands
{
	public class AddPaymentCommand : IRequest<string>
    {
        public decimal Value { get; set; }
        public string ClientUrl { get; set; }
    }
}
