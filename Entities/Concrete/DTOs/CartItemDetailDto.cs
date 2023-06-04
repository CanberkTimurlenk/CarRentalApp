using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs
{
    public class CartItemDetailDto
    {
        public int Id { get; set; }        
        public int CustomerId { get; set; }
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int TotalAmount { get; set; }        
        public string ColorName { get; set; }
        public string BrandName { get; set; }



    }
}
