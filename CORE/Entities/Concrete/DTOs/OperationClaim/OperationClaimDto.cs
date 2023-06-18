using Core.Entities;
using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.OperationClaim
{
    public record OperationClaimDto :IDto
    {
        public int Id { get; init; }
        public string OperationClaimName { get; init; }

    }
}
