using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService

    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;

        }

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

        public IDataResult<List<User>> GetAll()
        {

            return new SuccessDataResult<List<User>>(_userDal.GetAll(),Messages.UsersListed);
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserId == id), Messages.SuccessListedById);


            
        }

        public IResult Update(User updatedItem)
        {
            _userDal.Update(updatedItem);
            return new SuccessResult(Messages.UserUpdated);

        }
    }
}
