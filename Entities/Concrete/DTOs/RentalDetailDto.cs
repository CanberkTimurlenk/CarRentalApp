using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs
{
    public class RentalDetailDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }        
        public int PaymentId { get; set; }
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
