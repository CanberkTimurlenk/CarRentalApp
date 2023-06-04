using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartItemManager : ICartItemService
    {
        ICartItemDal _cartItemDal;

        public CartItemManager(ICartItemDal cartItemDal)
        {
            _cartItemDal = cartItemDal;
        }
        public IResult Add(CartItem addedItem)
        {
            _cartItemDal.Add(addedItem);
            return new SuccessResult();
        }

        public IResult Delete(CartItem deletedItem)
        {
            var result = _cartItemDal.Get(c => c.Id == deletedItem.Id);

            if(result != null) { 
            _cartItemDal.Delete(deletedItem);
                return new SuccessResult();
            }

            return new ErrorResult(Messages.CartItemNotExist);
        }

        public IDataResult<IEnumerable<CartItem>> GetAll()
        {

            return new SuccessDataResult<IEnumerable<CartItem>>(data: _cartItemDal.GetAll().ToList());
        }

        public IDataResult<CartItem> GetById(int id)
        {
            return new SuccessDataResult<CartItem>(data: _cartItemDal.Get(c => c.Id == id));

        }


        public IDataResult<IEnumerable<CartItemDetailDto>> GetCartItemDetailsByCustomerId(int id)
        {
            return new SuccessDataResult<IEnumerable<CartItemDetailDto>>(data: _cartItemDal.GetAllCartItemDetails(c => c.CustomerId == id));

        }

        public IResult Update(CartItem updatedItem)
        {
            var result = _cartItemDal.Get(c => c.Id == updatedItem.Id);

            if (result != null)
            {
                _cartItemDal.Delete(updatedItem);
                _cartItemDal.Add(updatedItem);
                return new SuccessResult();
            }

            return new ErrorResult(Messages.CartItemNotExist);

        }
    }
}
