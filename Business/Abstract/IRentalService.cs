﻿using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService : IBusinessRepository<Rental>
    {
        IDataResult<List<RentalDetailDto>> GetAllRentalDetails();

    }
}
