using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public UserManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IDataResult<int> Add(UserDtoForManipulation userDtoForManipulation)
        {
            var entity = _mapper.Map<User>(userDtoForManipulation);

            _manager.User.Add(entity);
            _manager.Save();

            int id = entity.Id;

            return new SuccessDataResult<int>(id, Messages.UserAdded);
        }
        public IResult Delete(int id, bool trackChanges)
        {
            var entity = _manager.User.Get(u => u.Id == id, trackChanges);

            _manager.User.Delete(entity);
            _manager.Save();

            return new SuccessResult(Messages.UserDeleted);
        }
        public (IDataResult<IEnumerable<UserDto>> result, MetaData metaData) GetAll(UserParameters userParameters, bool trackChanges)
        {
            var usersWithMetaData = _manager.User.GetAll(userParameters, trackChanges);

            var users = _mapper.Map<IEnumerable<UserDto>>(usersWithMetaData);

            return (new SuccessDataResult<IEnumerable<UserDto>>(users, Messages.CarsListed), usersWithMetaData.MetaData);

        }
        public IDataResult<UserDto> GetByEmail(string email, bool trackChanges)
        {
            var result = _manager.User.Get(u => u.Email == email, trackChanges);

            if (result is null)
                return new ErrorDataResult<UserDto>(Messages.DoesNotMatchAnUserAccount);

            return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(result));

        }
        public IDataResult<UserDto> GetById(int id, bool trackChanges)
        {
            var entity = _manager.User.Get(u => u.Id == id, trackChanges);

            var result = _mapper.Map<UserDto>(entity);

            return new SuccessDataResult<UserDto>(result, Messages.SuccessListedById);

        }
        public IDataResult<IEnumerable<OperationClaimDto>> GetOperationClaims(UserDto userDto, bool trackChanges)
        {
            var entity = _manager.User.Get(u => u.Id == userDto.Id, trackChanges);

            var result = _mapper.Map<IEnumerable<OperationClaimDto>>
                (_manager.User.GetOperationClaims(entity));

            if (!result.Any())
                return new ErrorDataResult<IEnumerable<OperationClaimDto>>
                    (result, Messages.UserOperationClaimNotFound);

            return new SuccessDataResult<IEnumerable<OperationClaimDto>>
                (result);

        }
        public IResult Update(int id, UserDtoForManipulation userDtoForManipulation, bool trackChanges)
        {
            var entity = _manager.User.Get(u => u.Id == id, trackChanges);

            var mappedEntity = _mapper.Map(userDtoForManipulation, entity);

            _manager.User.Update(mappedEntity);
            _manager.Save();

            return new SuccessResult(Messages.UserUpdated);

        }

    }
}
