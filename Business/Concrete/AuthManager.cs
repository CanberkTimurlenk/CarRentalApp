using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Concrete.DTOs;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetOperationClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user,claims);

            return new SuccessDataResult<AccessToken>(data:accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            
            var userToCheck = CheckIfUserExistsWithEmail(userForLoginDto.Email);
            if (userToCheck.Success == false) return new ErrorDataResult<User>();

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.WrongPassword);

            }
            return new SuccessDataResult<User>(userToCheck.Data,Messages.SuccessfullLogin);
            

 

        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            
            var userExist = CheckIfUserExistsWithEmail(userForRegisterDto.Email).Success;



            if (userExist == true) return new ErrorDataResult<User>(Messages.UserExists);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

             var user = new User()
             {
                 Email = userForRegisterDto.Email,
                 FirstName = userForRegisterDto.FirstName,
                 LastName = userForRegisterDto.LastName,
                 PasswordHash = passwordHash,
                 PasswordSalt = passwordSalt,
                 Status = true  //  keep true as default, it depends the system configuration
             };
            
            _userService.Add(user);

            return new SuccessDataResult<User>(user,Messages.UserRegistered);
          
        }

        private IDataResult<User> CheckIfUserExistsWithEmail(string email)
        {
            var result = _userService.GetByEmail(email);
            
             if (result.Data != null) return new SuccessDataResult<User>(result.Data, Messages.UserExists);
             return new ErrorDataResult<User>(Messages.UserNotExist);
                        

        }

   
    }
     
}
