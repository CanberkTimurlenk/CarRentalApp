using Core.Business;
using Core.Entities.Concrete;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.Rental;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService : IBusinessRepository<RentalDto,RentalDtoForManipulation,RentalParameters>
    {        
        (IDataResult<IEnumerable<RentalDetailDto>> result, MetaData metaData) GetAllRentalDetails(RentalParameters rentalParameters);

    }
}
