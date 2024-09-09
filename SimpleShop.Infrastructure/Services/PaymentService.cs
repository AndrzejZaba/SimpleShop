using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Shared.Payments.Commands;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Infrastructure.Services
{
    internal class PaymentService : IPaymentService
    {
        public string Create(string clientUrl, decimal value)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{clientUrl}potwierdzenie",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Quantity = 1,
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "pln",
                            UnitAmount = (long)(value * 100),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Rower 1"
                            }
                        }
                        
                    },
                },
                Mode = "payment"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return session.Id;
        }

        public bool IsPaid(string sessionId)
        {
			var service = new SessionService();
			var sessionDetails = service.Get(sessionId);
            return sessionDetails.PaymentStatus == "paid";
		}
    }
}
