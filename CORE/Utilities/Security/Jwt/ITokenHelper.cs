using Core.Entities.Concrete;
using Core.Entities.Concrete.DTOs.Token;
using Entities.Concrete.DTOs.OperationClaim;
using Entities.Concrete.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {

        public TokenDto CreateToken(UserDto userDto, IEnumerable<OperationClaimDto> operationClaims, DateTime userRefreshTokenExp, bool populateRefreshTokenExp);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
        bool CheckRefreshTokenIsValid(string providedRefreshToken, RefreshToken user);





    }
}
