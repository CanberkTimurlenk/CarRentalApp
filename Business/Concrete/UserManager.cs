using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.DTOs.OperationClaim;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Business.Concrete
{
    public class UserManager : IUserService

    {
        //get operation claims and get by email added
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;

        }

        [ValidationAspect(typeof(UserValidator))]
        public IDataResult<int> Add(UserDtoForManipulation userDtoForManipulation)
        {
            var entity = _mapper.Map<User>(userDtoForManipulation);

            _userDal.Add(entity);

            int id = entity.Id;

            return new SuccessDataResult<int>(id, Messages.UserAdded);
        }

        public IResult Delete(int id)
        {
            var entity = _userDal.Get(u => u.Id == id);

            _userDal.Delete(entity);

            return new SuccessResult(Messages.UserDeleted);
        }
        public IDataResult<IEnumerable<UserDto>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<UserDto>>(_userDal.GetAll());

            return new SuccessDataResult<IEnumerable<UserDto>>(result, Messages.UsersListed);

        }
        public IDataResult<UserDto> GetByEmail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);

            if (result == null)
                return new ErrorDataResult<UserDto>(Messages.DoesNotMatchAnUserAccount);

            return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(result));

        }
        public IDataResult<UserDto> GetById(int id)
        {
            var entity = _userDal.Get(u => u.Id == id);
            var result = _mapper.Map<UserDto>(entity);

            return new SuccessDataResult<UserDto>(result, Messages.SuccessListedById);

        }
        public IDataResult<IEnumerable<OperationClaimDto>> GetOperationClaims(UserDto userDto)
        {
            var entity = _userDal.Get(u => u.Id == userDto.Id);

            var result = _mapper.Map<IEnumerable<OperationClaimDto>>(_userDal.GetOperationClaims(entity));

            if (!result.Any())
                return new ErrorDataResult<IEnumerable<OperationClaimDto>>(result, Messages.UserOperationClaimNotFound);

            return new SuccessDataResult<IEnumerable<OperationClaimDto>>(result);

        }
        public IResult Update(int id, UserDtoForManipulation userDtoForManipulation)
        {
            var entity = _userDal.Get(u => u.Id == id);

            var mappedEntity = _mapper.Map(userDtoForManipulation, entity);

            _userDal.Update(mappedEntity);
            return new SuccessResult(Messages.UserUpdated);

        }

    }
}
