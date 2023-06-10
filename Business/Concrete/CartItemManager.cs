using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Business;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.CartItem;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace Business.Concrete
{
    public class CartItemManager : ICartItemService
    {
        private readonly ICartItemDal _cartItemDal;
        private readonly IMapper _mapper;

        public CartItemManager(ICartItemDal cartItemDal, IMapper mapper)
        {
            _cartItemDal = cartItemDal;
            _mapper = mapper;
        }
        public IDataResult<int> Add(CartItemDtoForManipulation carItemDtoForManipulation)
        {
            var entity = _mapper.Map<CartItem>(carItemDtoForManipulation);

            _cartItemDal.Add(entity);

            return new SuccessDataResult<int>(entity.Id,Messages.CartItemAdded);
        }

        public IResult Delete(int id)
        {
            var entity = _cartItemDal.Get(c => c.Id == id);

            if (entity is not null)
            {
                _cartItemDal.Delete(entity);
                return new SuccessResult();
            }

            return new ErrorResult(Messages.CartItemNotExist);
        }

        public (IDataResult<IEnumerable<CartItemDto>> result, MetaData metaData) GetAll(CartItemParameters cartItemParameters)
        {
            var cartItemsWithMetaData = _cartItemDal.GetAll(cartItemParameters);
            var cartItems = _mapper.Map<IEnumerable<CartItemDto>>(cartItemsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CartItemDto>>(cartItems, Messages.CarsListed), cartItemsWithMetaData.MetaData);

        }

        public IDataResult<CartItemDto> GetById(int id)
        {
            var entity = _cartItemDal.Get(c => c.Id == id);

            var result = _mapper.Map<CartItemDto>(entity);

            return new SuccessDataResult<CartItemDto>(result);

        }


        public (IDataResult<IEnumerable<CartItemDetailDto>>, MetaData metaData) GetCartItemDetailsByCustomerId(int id,CartItemParameters cartItemParameters)
        {
            var cartItemsWithMetaData = _cartItemDal.GetAll(cartItemParameters, c => c.CustomerId == id);

            var cartItems = _mapper.Map<IEnumerable<CartItemDetailDto>>(cartItemsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CartItemDetailDto>>(cartItems), cartItemsWithMetaData.MetaData);

        }

        public IResult Update(int id, CartItemDtoForManipulation cartItemDtoForManipulation)
        {
            var entity = _cartItemDal.Get(c => c.Id == id);

            if (entity is not null)
            {
                var mappedEntity = _mapper.Map(cartItemDtoForManipulation, entity);
                _cartItemDal.Update(mappedEntity);
                return new SuccessResult();
            }

            return new ErrorResult(Messages.CartItemNotExist);

        }


    }
}
