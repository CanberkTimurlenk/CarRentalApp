using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.DTOs.Token;
using Core.Entities.Concrete.DTOs.User;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.Models;
using System.Security.Claims;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper, ITokenService tokenService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public IDataResult<TokenDto> CreateToken(UserDto user, bool populateExp)
        {
            var claims = _userService.GetOperationClaims(user, false).Data;

            var refreshTokenExp = _tokenService.GetRefreshTokenByEmail(user.Email,out _).Data.Expiration;

            var token = _tokenHelper.CreateToken(user, claims, refreshTokenExp, populateExp);

            _tokenService.SetRefreshTokenByEmail(user.Email, token.RefreshToken);

            return new SuccessDataResult<TokenDto>(data: token, Messages.TokenCreated);
        }

        public IDataResult<TokenDto> RefreshToken(TokenForRefreshDto tokenForRefreshDto)
        {
            var principal = _tokenHelper.GetPrincipalFromExpiredToken(tokenForRefreshDto.AccessToken);

            var email = principal.FindFirst(ClaimTypes.Email).Value;

            var storedRefreshToken = _tokenService.GetRefreshTokenByEmail(email,out var user).Data;

            var refreshTokenIsValid = _tokenHelper.CheckRefreshTokenIsValid(tokenForRefreshDto.RefreshToken, storedRefreshToken);

            if (!refreshTokenIsValid)
                return new ErrorDataResult<TokenDto>(Messages.RefreshTokenIsNotValid);            

            return new SuccessDataResult<TokenDto>(CreateToken(user, false).Data);

        }

        public IDataResult<UserDto> Login(UserForLoginDto userForLoginDto)
        {

            var userToCheck = CheckIfUserExistsWithEmail(userForLoginDto.Email);

            if (!userToCheck.Success)
                return new ErrorDataResult<UserDto>();

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
                return new ErrorDataResult<UserDto>(Messages.WrongPassword);

            return new SuccessDataResult<UserDto>(userToCheck.Data, Messages.SuccessfullLogin);

        }

        public IDataResult<UserDto> Register(UserForRegisterDto userForRegisterDto)
        {

            var userExist = CheckIfUserExistsWithEmail(userForRegisterDto.Email).Success;

            if (userExist == true)
                return new ErrorDataResult<UserDto>(Messages.UserExists);

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

            var userDtoForManipulation = _mapper.Map<UserForManipulationDto>(user);

            var userDto = _mapper.Map<UserDto>(userDtoForManipulation);
            int userId = _userService.Add(userDtoForManipulation).Data;

            return new SuccessDataResult<UserDto>(userDto with { Id = userId }, Messages.UserRegistered);

        }

        private IDataResult<UserDto> CheckIfUserExistsWithEmail(string email)
        {
            var result = _userService.GetByEmail(email, false);

            if (result.Data != null)
                return new SuccessDataResult<UserDto>(result.Data, Messages.UserExists);

            return new ErrorDataResult<UserDto>(Messages.UserNotExist);

        }
    }
}
