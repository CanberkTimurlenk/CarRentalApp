using Core.DataAccess;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using System.Linq.Expressions;


namespace DataAccess.Abstract
{
    public interface ICartItemDal : IRepositoryBase<CartItem, CartItemParameters>
    {
        IEnumerable<CartItemDetailDto> GetAllCartItemDetails(Expression<Func<CartItemDetailDto, bool>> filter = null);
    }


}
