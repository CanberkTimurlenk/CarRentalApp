﻿using Core.Business;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.CartItem;
using Entities.Concrete.RequestFeatures;


namespace Business.Abstract
{
    public interface ICartItemService: IBusinessRepository<CartItemDto, CartItemForManipulationDto,CartItemParameters>
    {
        Task<(IDataResult<IEnumerable<CartItemDetailDto>>, MetaData metaData)> GetCartItemDetailsByCustomerIdAsync(int id, CartItemParameters cartItemParameters, bool trackChanges);
       
    }
}
