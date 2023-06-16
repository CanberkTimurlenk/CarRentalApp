using Core.Entities;

namespace Entities.Concrete.DTOs.Rental
{
    public record RentalDto : IDto
    {
        public int Id { get; init; }
        public int CarId { get; init; }
        public int CustomerId { get; init; }
        public DateTime RentDate { get; init; }
        public DateTime? ReturnDate { get; init; }         

    }
}
