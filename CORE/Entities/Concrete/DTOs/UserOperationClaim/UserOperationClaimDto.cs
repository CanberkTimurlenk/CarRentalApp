using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.UserOperationClaim
{
    public record UserOperationClaimDto : IDto
    {
        public int Id { get; init; }
        public int OperationClaimId { get; init; }        
        public int UserId { get; init; }
    }
}
