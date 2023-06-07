using Core.Entities;

namespace Entities.Concrete.DTOs.Rental
{
    public record RentalDtoForManipulation : IDto
    {        
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
