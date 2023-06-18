using Core.Entities.Abstract;

namespace Entities.Concrete.Models
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string OperationClaimName { get; set; }

        public ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
