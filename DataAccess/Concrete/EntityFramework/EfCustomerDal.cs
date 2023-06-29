using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer>, ICustomerDal
    {
        public EfCustomerDal(CarAppContext context) : base(context)
        {

        }

    }
}
