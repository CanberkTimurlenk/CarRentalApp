using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.OperationClaim;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.RequestFeatures;

namespace Business.Abstract
{
    public interface IUserService : IBusinessRepository<UserDto, UserForManipulationDto, UserParameters>
    {
        IDataResult<IEnumerable<OperationClaimDto>> GetOperationClaims(UserDto userDto, bool trackChanges);
        IDataResult<UserDto> GetByEmail(string email, bool trackChanges);

    }
}
