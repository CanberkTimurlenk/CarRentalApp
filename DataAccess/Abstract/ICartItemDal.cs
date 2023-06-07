using Core.DataAccess;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICartItemDal : IEntityRepository<CartItem>
    {
        IEnumerable<CartItemDetailDto> GetAllCartItemDetails(Expression<Func<CartItemDetailDto, bool>> filter = null);
    } 

        
}
