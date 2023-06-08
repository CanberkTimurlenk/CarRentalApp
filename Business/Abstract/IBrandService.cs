using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Entities.Concrete.DTOs.Brand;
using Entities.Concrete.Models;

namespace Business.Abstract
{
    public interface IBrandService : IBusinessRepository<BrandDto,BrandDtoForManipulation>
    {

    }
}
