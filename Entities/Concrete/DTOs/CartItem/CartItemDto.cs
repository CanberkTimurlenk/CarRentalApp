using Core.Entities;

namespace Entities.Concrete.DTOs.CartItem
{
    public record CartItemDto : IDto
    {
        public int Id { get; init; }
        public int CarId { get; init; }
        public int CustomerId { get; init; }
        public decimal TotalAmount { get; init; }

    }
}
