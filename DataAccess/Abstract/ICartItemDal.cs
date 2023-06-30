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
        Task<PagedList<CartItemDetailDto>> GetAllCartItemDetails(CartItemParameters cartItemParameters, bool trackChanges);
        Task<PagedList<CartItemDetailDto>> GetAllCartItemDetailsByCondition(Expression<Func<CartItemDetailDto, bool>> filter, CartItemParameters cartItemParameters, bool trackChanges);
    }
}
