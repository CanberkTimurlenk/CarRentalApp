using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.DTOs;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.Models;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public IDataResult<AccessToken> CreateAccessToken(UserDto userDto)
        {
            var claims = _userService.GetOperationClaims(userDto, false).Data;

            var accessToken = _tokenHelper.CreateToken(userDto, claims);

            return new SuccessDataResult<AccessToken>(data: accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<UserDto> Login(UserForLoginDto userForLoginDto)
        {

            var userToCheck = CheckIfUserExistsWithEmail(userForLoginDto.Email);

            if (userToCheck.Success == false)
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

            var userDtoForManipulation = _mapper.Map<UserDtoForManipulation>(user);
            var userDto = _mapper.Map<UserDto>(userDtoForManipulation);

            userDto.Id = _userService.Add(userDtoForManipulation).Data;


            return new SuccessDataResult<UserDto>(userDto, Messages.UserRegistered);


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
