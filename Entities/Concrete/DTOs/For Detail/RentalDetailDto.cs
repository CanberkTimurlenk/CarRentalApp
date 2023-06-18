using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs
{
    public record RentalDetailDto: IDto
    {
        public int Id { get; init; }
        public int CarId { get; init; }                
        public int CustomerId { get; init; }
        public int TotalAmount { get; init; }        
        public string BrandName { get; init; }
        public string ModelName { get; init; }
        public string? CompanyName { get; init; }
        public string CustomerFirstName { get; init; }
        public string CustomerLastName { get; init; }
        public DateTime RentDate { get; init; }
        public DateTime? ReturnDate { get; init; } 

    }
}
