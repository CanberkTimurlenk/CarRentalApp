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

        public async Task<IDataResult<int>> AddAsync(CartItemForManipulationDto carItemDtoForManipulation)
        {
            var entity = _mapper.Map<CartItem>(carItemDtoForManipulation);

            await _manager.CartItem.AddAsync(entity);
            await _manager.SaveAsync();

            return new SuccessDataResult<int>(entity.Id, Messages.CartItemAdded);
        }
        public async Task<IResult> DeleteAsync(int id, bool trackChanges)
        {
            var entity = await _manager.CartItem.GetAsync(c => c.Id == id, trackChanges);

            if (entity is not null)
            {
                _manager.CartItem.Delete(entity);
                await _manager.SaveAsync();

                return new SuccessResult();
            }

            return new ErrorResult(Messages.CartItemNotExist);
        }
        public async Task<(IDataResult<IEnumerable<CartItemDto>> result, MetaData metaData)> GetAllAsync(CartItemParameters cartItemParameters, bool trackChanges)
        {
            var cartItemsWithMetaData = await _manager.CartItem.GetAllAsync(cartItemParameters, trackChanges);
            var cartItems = _mapper.Map<IEnumerable<CartItemDto>>(cartItemsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CartItemDto>>(cartItems, Messages.CarsListed), cartItemsWithMetaData.MetaData);

        }
        public async Task<IDataResult<CartItemDto>> GetByIdAsync(int id, bool trackChanges)
        {
            var entity = await _manager.CartItem.GetAsync(c => c.Id == id, trackChanges);

            var result = _mapper.Map<CartItemDto>(entity);

            return new SuccessDataResult<CartItemDto>(result);

        }
        public async Task<(IDataResult<IEnumerable<CartItemDetailDto>>, MetaData metaData)> GetCartItemDetailsByCustomerIdAsync(int id, CartItemParameters cartItemParameters, bool trackChanges)
        {
            var cartItemsWithMetaData = await _manager.CartItem.GetAllCartItemDetailsByCondition(c => c.CustomerId == id, cartItemParameters, trackChanges);

            var cartItems = _mapper.Map<IEnumerable<CartItemDetailDto>>(cartItemsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CartItemDetailDto>>(cartItems), cartItemsWithMetaData.MetaData);

        }
        public async Task<IResult> UpdateAsync(int id, CartItemForManipulationDto cartItemDtoForManipulation, bool trackChanges)
        {
            var entity = await _manager.CartItem.GetAsync(c => c.Id == id, trackChanges);

            if (entity is not null)
            {
                var mappedEntity = _mapper.Map(cartItemDtoForManipulation, entity);

                _manager.CartItem.Update(mappedEntity);
                await _manager.SaveAsync();

                return new SuccessResult();
            }

            return new ErrorResult(Messages.CartItemNotExist);

        }

    }
}
