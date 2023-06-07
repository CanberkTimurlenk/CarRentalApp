using Core.Entities;

namespace Entities.Concrete.DTOs.CartItem
{
    public record CartItemDto : IDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
