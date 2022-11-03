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
        public string BrandName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; } //DateTime? bu değer null da olabilir anlamına gelir
    }
}
