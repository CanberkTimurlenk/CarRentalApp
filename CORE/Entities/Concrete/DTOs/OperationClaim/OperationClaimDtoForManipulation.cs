using Core.Entities;
using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.OperationClaim
{
    public record OperationClaimDtoForManipulation:IDto
    {        
        public string OperationClaimName { get; init; }

    }
}
