using Core.Entities.Concrete.DTOs.Token;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.User;

namespace Business.Abstract
{
    public interface ITokenService
    {
        Task<IDataResult<RefreshToken>> GetRefreshTokenByEmailAsync(string email);
        Task<IResult> SetRefreshTokenByEmailAsync(string email, RefreshToken refreshToken);

    }
}