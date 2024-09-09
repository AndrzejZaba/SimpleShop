using SimpleShop.Application.Common.Interfaces;


namespace SimpleShop.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
