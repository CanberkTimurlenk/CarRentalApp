using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
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
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;

        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User addedItem)
        {
            _userDal.Add(addedItem);
            return new SuccessResult(Messages.UserAdded);
        }
        public IResult Delete(User deletedItem)
        {
            _userDal.Delete(deletedItem);
            return new SuccessResult(Messages.UserDeleted);
        }
        public IDataResult<IEnumerable<User>> GetAll()
        {

            return new SuccessDataResult<IEnumerable<User>>(_userDal.GetAll(),Messages.UsersListed);
        }
        public IDataResult<User> GetByEmail(string email)
        {
            
            var result = _userDal.Get(u => u.Email == email);

            if(result == null) return new ErrorDataResult<User>(Messages.DoesNotMatchAnUserAccount);

            return new SuccessDataResult<User>(result);
            
            //return _userDal.Get(u => u.Email == email);
        }   
        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id), Messages.SuccessListedById);


            
        }
        public IDataResult<IEnumerable<OperationClaim>> GetOperationClaims(User user)
        {
            var result = _userDal.GetOperationClaims(user);

            if (!result.Any())
            { 
                return new ErrorDataResult<IEnumerable<OperationClaim>>(result,Messages.UserOperationClaimNotFound); 
            }

            return new SuccessDataResult<IEnumerable<OperationClaim>>(result);

            

        }
        public IResult Update(User updatedItem)
        {
            _userDal.Update(updatedItem);
            return new SuccessResult(Messages.UserUpdated);

        }


    }
}
