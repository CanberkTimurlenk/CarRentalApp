using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete.Models;

namespace DataAccess.Abstract
{
    public interface IColorDal : IRepositoryBase<Color>
    {
    }
}
