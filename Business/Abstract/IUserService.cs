using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.OperationClaim;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.RequestFeatures;

namespace Business.Abstract
{
    public interface IUserService : IBusinessRepository<UserDto,UserDtoForManipulation,UserParameters>
    {
        IDataResult<IEnumerable<OperationClaimDto>> GetOperationClaims (UserDto userDto);
        IDataResult<UserDto> GetByEmail(string email);        

    }
}
