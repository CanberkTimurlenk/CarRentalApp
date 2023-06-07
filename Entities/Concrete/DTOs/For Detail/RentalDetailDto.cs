using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public record RentalDetailDto: IDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }                
        public int CustomerId { get; set; }
        public int TotalAmount { get; set; }        
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string? CompanyName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; } 

    }
}
