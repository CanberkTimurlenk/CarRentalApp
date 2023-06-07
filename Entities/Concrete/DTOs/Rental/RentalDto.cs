using Core.Entities;

namespace Entities.Concrete.DTOs.Rental
{
    public record RentalDto : IDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }         

    }
}
