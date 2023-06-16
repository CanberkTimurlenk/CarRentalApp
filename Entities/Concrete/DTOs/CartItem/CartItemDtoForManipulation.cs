using Core.Entities;

namespace Entities.Concrete.DTOs.CartItem
{
    public record CartItemDtoForManipulation: IDto
    {
        public int CarId { get; init; }
        public int CustomerId { get; init; }
        public decimal TotalAmount { get; init; }
    }
}
