using Core.Entities;

namespace Entities.Concrete.DTOs.OperationClaim
{
    public record OperationClaimDto :IDto
    {
        public int Id { get; set; }
        public string OperationClaimName { get; set; }

    }
}
