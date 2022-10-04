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

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal : EfEntityRepositoryBase <Brand,CarAppContext> , IBrandDal
    //  EfCarDal : Entity Framework Data Access Layer
    //  EfEntityRepositoryBase EntityFramework için yaratılan base class..
    //  EntityFramework methodlarını tutar (Repository)
    //  EfCarDal inherits EfEntityRepositoryBase with "Brand" and "CarpAppContext" and implements "IBrandDal"
    {

    }
}
