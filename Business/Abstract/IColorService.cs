using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Entities.Concrete.DTOs.Color;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace Business.Abstract
{
    public interface IColorService : IBusinessRepository<ColorDto,ColorDtoForManipulation,ColorParameters>
    {

    }
}
