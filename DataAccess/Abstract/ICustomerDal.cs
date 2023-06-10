using Core.DataAccess;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IRepositoryBase<Customer,CustomerParamaters>
    {

    }
}
