using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICartItemDal : IRepositoryBase<CartItem>
    {
        PagedList<CartItemDetailDto> GetAllCartItemDetails(CartItemParameters cartItemParameters, bool trackChanges);
        PagedList<CartItemDetailDto> GetAllCartItemDetailsByCondition(Expression<Func<CartItemDetailDto, bool>> filter, CartItemParameters cartItemParameters, bool trackChanges);
    }
}
