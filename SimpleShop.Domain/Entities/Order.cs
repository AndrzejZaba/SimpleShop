

namespace SimpleShop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string UserId { get; set; }
        public bool IsPaid { get; set; }
        public string SessionId { get; set; }
    }
}
