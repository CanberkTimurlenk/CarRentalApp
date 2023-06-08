﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using Entities.Concrete.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfColorDal : EfEntityRepositoryBase<Color,CarAppContext> , IColorDal    
    {
        public EfColorDal(IDesignTimeDbContextFactory<CarAppContext> contextFactory)
            : base(contextFactory)
        {


        }
    }
}
