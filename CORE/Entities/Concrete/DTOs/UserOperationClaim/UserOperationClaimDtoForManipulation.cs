using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.UserOperationClaim
{
    public record UserOperationClaimDtoForManipulation : IDto
    {
        public int OperationClaimId { get; init; }
        public int UserId { get; init; }
    }
}
