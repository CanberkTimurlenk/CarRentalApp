using Core.Entities.Abstract;

namespace Entities.Concrete.Models
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public ICollection<Rental> Rentals { get; set; }

    }
}
