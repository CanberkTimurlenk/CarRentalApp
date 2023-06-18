using Core.Entities.Abstract;

namespace Entities.Concrete.Models
{
    public class Rental : IEntity, IQuerySortable
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; } //DateTime? bu değer null da olabilir anlamına gelir

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
