using Core.Entities;

namespace Entities.Concrete.DTOs.CartItem
{
    public record CartItemDtoForManipulation: IDto
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
