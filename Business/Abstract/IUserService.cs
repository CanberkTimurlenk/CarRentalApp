using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.OperationClaim;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.RequestFeatures;

namespace Business.Abstract
{
    public interface IUserService : IBusinessRepository<UserDto, UserForManipulationDto, UserParameters>
    {
        Task<IDataResult<IEnumerable<OperationClaimDto>>> GetOperationClaimsAsync(UserDto userDto, bool trackChanges);
        Task<IDataResult<UserDto>> GetByEmailAsync(string email, bool trackChanges);

    }
}
