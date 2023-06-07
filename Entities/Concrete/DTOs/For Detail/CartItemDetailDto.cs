using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public record CartItemDetailDto : IDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public decimal TotalAmount { get; set; }
        public string ColorName { get; set; }
        public string BrandName { get; set; }



    }
}
