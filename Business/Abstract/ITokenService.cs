using Core.Entities.Concrete.DTOs.Token;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.User;

namespace Business.Abstract
{
    public interface ITokenService
    {
        IDataResult<RefreshToken> GetRefreshTokenByEmail(string email, out UserDto userDto);
        IResult SetRefreshTokenByEmail(string email, RefreshToken refreshToken);

    }
}