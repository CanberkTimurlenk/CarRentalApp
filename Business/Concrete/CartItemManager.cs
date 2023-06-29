using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract.RepositoryManager;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.CartItem;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace Business.Concrete
{
    public class CartItemManager : ICartItemService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CartItemManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public IDataResult<int> Add(CartItemForManipulationDto carItemDtoForManipulation)
        {
            var entity = _mapper.Map<CartItem>(carItemDtoForManipulation);

            _manager.CartItem.Add(entity);
            _manager.Save();

            return new SuccessDataResult<int>(entity.Id, Messages.CartItemAdded);
        }
        public IResult Delete(int id, bool trackChanges)
        {
            var entity = _manager.CartItem.Get(c => c.Id == id, trackChanges);

            if (entity is not null)
            {
                _manager.CartItem.Delete(entity);
                _manager.Save();

                return new SuccessResult();
            }

            return new ErrorResult(Messages.CartItemNotExist);
        }
        public (IDataResult<IEnumerable<CartItemDto>> result, MetaData metaData) GetAll(CartItemParameters cartItemParameters, bool trackChanges)
        {
            var cartItemsWithMetaData = _manager.CartItem.GetAll(cartItemParameters, trackChanges);
            var cartItems = _mapper.Map<IEnumerable<CartItemDto>>(cartItemsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CartItemDto>>(cartItems, Messages.CarsListed), cartItemsWithMetaData.MetaData);

        }
        public IDataResult<CartItemDto> GetById(int id, bool trackChanges)
        {
            var entity = _manager.CartItem.Get(c => c.Id == id, trackChanges);

            var result = _mapper.Map<CartItemDto>(entity);

            return new SuccessDataResult<CartItemDto>(result);

        }
        public (IDataResult<IEnumerable<CartItemDetailDto>>, MetaData metaData) GetCartItemDetailsByCustomerId(int id, CartItemParameters cartItemParameters, bool trackChanges)
        {
            var cartItemsWithMetaData = _manager.CartItem.GetAllByCondition(c => c.CustomerId == id, cartItemParameters, trackChanges);

            var cartItems = _mapper.Map<IEnumerable<CartItemDetailDto>>(cartItemsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CartItemDetailDto>>(cartItems), cartItemsWithMetaData.MetaData);

        }
        public IResult Update(int id, CartItemForManipulationDto cartItemDtoForManipulation, bool trackChanges)
        {
            var entity = _manager.CartItem.Get(c => c.Id == id, trackChanges);

            if (entity is not null)
            {
                var mappedEntity = _mapper.Map(cartItemDtoForManipulation, entity);

                _manager.CartItem.Update(mappedEntity);
                _manager.Save();

                return new SuccessResult();
            }

            return new ErrorResult(Messages.CartItemNotExist);

        }

    }
}
