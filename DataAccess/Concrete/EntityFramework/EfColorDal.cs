using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfColorDal : EfEntityRepositoryBase<Color, ColorParameters>, IColorDal
    {
        public EfColorDal(CarAppContext context) : base(context)
        {

        }

    }
}
