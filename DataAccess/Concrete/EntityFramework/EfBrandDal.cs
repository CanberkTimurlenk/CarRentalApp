using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal : EfEntityRepositoryBase<Brand>, IBrandDal
    {
        public EfBrandDal(CarAppContext context) : base(context)
        {

        }
    }
}
