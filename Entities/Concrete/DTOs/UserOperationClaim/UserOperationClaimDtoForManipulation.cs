using Core.Entities;

namespace Entities.Concrete.DTOs.UserOperationClaim
{
    public record UserOperationClaimDtoForManipulation : IDto
    {
        public int OperationClaimId { get; set; }
        public int UserId { get; set; }
    }
}
