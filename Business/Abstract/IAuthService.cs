﻿using Core.Entities.Concrete;
using Core.Entities.Concrete.DTOs;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.Jwt;
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
        IDataResult<UserDto> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<UserDto> Login(UserForLoginDto userForLoginDto);
        IDataResult<AccessToken> CreateAccessToken(UserDto userDto);
        
    }
}
