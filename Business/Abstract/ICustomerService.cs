using Core.Business;
using Entities.Concrete.DTOs.Customer;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService : IBusinessRepository<CustomerDto, CustomerForManipulationDto, CustomerParamaters>
    {


    }
}
