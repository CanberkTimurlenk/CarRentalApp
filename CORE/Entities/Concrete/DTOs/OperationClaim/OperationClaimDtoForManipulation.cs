using Core.Entities;

namespace Entities.Concrete.DTOs.OperationClaim
{
    public record OperationClaimDtoForManipulation:IDto
    {        
        public string OperationClaimName { get; init; }

    }
}
