using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService : IBusinessRepository<User>
    {

        IDataResult<IEnumerable<OperationClaim>> GetOperationClaims (User user);
        IDataResult<User> GetByEmail(string email);


    }
}
