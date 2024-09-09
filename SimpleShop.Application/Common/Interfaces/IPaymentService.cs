using SimpleShop.Shared.Payments.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Common.Interfaces
{
    public interface IPaymentService
    {
        string Create(string clientUrl, decimal value);
        bool IsPaid(string sessionId);

	}
}
