
using Core.Entities;

namespace Entities.Concrete.DTOs.UserOperationClaim
{
    public record UserOperationClaimDto : IDto
    {
        public int Id { get; set; }
        public int OperationClaimId { get; set; }        
        public int UserId { get; set; }
    }
}
