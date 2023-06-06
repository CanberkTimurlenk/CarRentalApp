using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfColorDal : EfEntityRepositoryBase<Color,CarAppContext> , IColorDal
    //  EfColorDal : Entity Framework Data Access Layer
    //  EfEntityRepositoryBase EntityFramework için yaratılan base class..
    //  EntityFramework methodlarını tutar (Repository)
    //  EfColorDal inherits EfEntityRepositoryBase with "Color" and "CarpAppContext" and implements "IColorDal"
    {
        public EfColorDal(IDesignTimeDbContextFactory<CarAppContext> contextFactory)
            : base(contextFactory)
        {

        }




    }
}
