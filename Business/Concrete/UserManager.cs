using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete.DTOs.Token;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract.RepositoryManager;
using Entities.Concrete.DTOs.OperationClaim;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace Business.Concrete
{
    public class UserManager : IUserService, ITokenService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public UserManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(UserValidator))]
        public async Task<IDataResult<int>> AddAsync(UserForManipulationDto userDtoForManipulation)
        {
            var entity = _mapper.Map<User>(userDtoForManipulation);

            await _manager.User.AddAsync(entity);
            await _manager.SaveAsync();

            int id = entity.Id;
            //  null check !!!!

            return new SuccessDataResult<int>(id, Messages.UserAdded);
        }
        public async Task<IResult> DeleteAsync(int id, bool trackChanges)
        {
            var entity = await _manager.User.GetAsync(u => u.Id == id, trackChanges);

            _manager.User.Delete(entity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.UserDeleted);
        }
        public async Task<(IDataResult<IEnumerable<UserDto>> result, MetaData metaData)> GetAllAsync(UserParameters userParameters, bool trackChanges)
        {
            var usersWithMetaData = await _manager.User.GetAllAsync(userParameters, trackChanges);

            var users = _mapper.Map<IEnumerable<UserDto>>(usersWithMetaData);

            return (new SuccessDataResult<IEnumerable<UserDto>>(users, Messages.CarsListed), usersWithMetaData.MetaData);

        }
        public async Task<IDataResult<UserDto>> GetByEmailAsync(string email, bool trackChanges)
        {
            var result = await _manager.User.GetAsync(u => u.Email == email, trackChanges);

            if (result is null)
                return new ErrorDataResult<UserDto>(Messages.DoesNotMatchAnUserAccount);

            return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(result));

        }
        public async Task<IDataResult<UserDto>> GetByIdAsync(int id, bool trackChanges)
        {
            var entity = await _manager.User.GetAsync(u => u.Id == id, trackChanges);

            var result = _mapper.Map<UserDto>(entity);

            return new SuccessDataResult<UserDto>(result, Messages.SuccessListedById);

        }
        public async Task<IDataResult<IEnumerable<OperationClaimDto>>> GetOperationClaimsAsync(UserDto userDto, bool trackChanges)
        {
            var entity = await _manager.User.GetAsync(u => u.Id == userDto.Id, trackChanges);

            var result = _mapper.Map<IEnumerable<OperationClaimDto>>(
                await _manager.User.GetOperationClaims(entity)
                );

            if (!result.Any())
                return new ErrorDataResult<IEnumerable<OperationClaimDto>>
                    (result, Messages.UserOperationClaimNotFound);

            return new SuccessDataResult<IEnumerable<OperationClaimDto>>
                (result);

        }
        public async Task<IResult> UpdateAsync(int id, UserForManipulationDto userDtoForManipulation, bool trackChanges)
        {
            var entity = await _manager.User.GetAsync(u => u.Id == id, trackChanges);

            var mappedEntity = _mapper.Map(userDtoForManipulation, entity);

            _manager.User.Update(mappedEntity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.UserUpdated);

        }

        public async Task<IDataResult<RefreshToken>> GetRefreshTokenByEmailAsync(string email)
        {

            var user = await _manager.User.GetAsync(u => u.Email == email, false);

            return new SuccessDataResult<RefreshToken>(
                new RefreshToken
                {
                    Expiration = user.RefreshTokenExpiration,
                    Token = user.RefreshToken
                });

        }

        public async Task<IResult> SetRefreshTokenByEmailAsync(string email, RefreshToken refreshToken)
        {
            var user = await _manager.User.GetAsync(u => u.Email == email, false);

            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenExpiration = refreshToken.Expiration;

            _manager.User.Update(user);
            await _manager.SaveAsync();

            return new SuccessResult();

        }
    }
}
