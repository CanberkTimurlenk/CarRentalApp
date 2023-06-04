using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }


        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer addedItem)
        {
            _customerDal.Add(addedItem);

            return new SuccessResult(Messages.CustomerAdded);
        }

        public IResult Delete(Customer deletedItem)
        {
            _customerDal.Delete(deletedItem);

            return new SuccessResult(Messages.CustomerDeleted);
        }

        public IDataResult<IEnumerable<Customer>> GetAll()
        {
            return new SuccessDataResult<IEnumerable<Customer>>(_customerDal.GetAll(), Messages.CustomersListed);
          
        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == id),Messages.SuccessListedById);
        }

        public IResult Update(Customer updatedItem)
        {
            _customerDal.Update(updatedItem);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
