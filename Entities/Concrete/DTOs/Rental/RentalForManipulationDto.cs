using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Rental
{
    public record RentalForManipulationDto : IDto
    {        
        public int CarId { get; init; }
        public int CustomerId { get; init; }
        public DateTime RentDate { get; init; }
        public DateTime? ReturnDate { get; init; }

    }
}
