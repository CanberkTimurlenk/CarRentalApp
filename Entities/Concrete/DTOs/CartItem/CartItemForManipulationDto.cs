using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.CartItem
{
    public record CartItemForManipulationDto: IDto
    {
        public int CarId { get; init; }
        public int CustomerId { get; init; }
        public decimal TotalAmount { get; init; }
    }
}
