﻿using Core.Business;
using Entities.Concrete.DTOs.CartItem;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartItemService: IBusinessRepository<CartItemDto, CartItemDtoForManipulation>
    {

    }
}
