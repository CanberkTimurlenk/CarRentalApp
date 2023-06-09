using Core.Entities.Concrete;
using Entities.Concrete.DTOs.OperationClaim;
using Entities.Concrete.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {

        AccessToken CreateToken(UserDto userDto, IEnumerable<OperationClaimDto> operationClaims);
      

    }
}
