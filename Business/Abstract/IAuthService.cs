﻿using Core.Entities.Concrete;
using Core.Entities.Concrete.DTOs.Token;
using Core.Entities.Concrete.DTOs.User;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<UserDto>> Register(UserForRegisterDto userForRegisterDto);
        Task<IDataResult<UserDto>> Login(UserForLoginDto userForLoginDto);
        Task<IDataResult<TokenDto>> CreateToken(UserDto userDto, bool populateExp);
        Task<IDataResult<TokenDto>> RefreshToken(TokenForRefreshDto refreshTokenDto);

    }
}
