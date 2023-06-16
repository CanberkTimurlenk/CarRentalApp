using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public record CartItemDetailDto : IDto
    {
        public int Id { get; init; }
        public int CustomerId { get; init; }
        public int RentalId { get; init; }
        public int CarId { get; init; }
        public decimal TotalAmount { get; init; }
        public string ColorName { get; init; }
        public string BrandName { get; init; }



    }
}
