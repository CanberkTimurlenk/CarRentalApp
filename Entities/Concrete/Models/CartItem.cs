using Core.Entities.Abstract;

namespace Entities.Concrete.Models
{
    public class CartItem : IEntity
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
