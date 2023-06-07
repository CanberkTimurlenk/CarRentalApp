using Core.Entities.Concrete;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {

        AccessToken CreateToken(User user, IEnumerable<OperationClaim> operationClaims);
      

    }
}
