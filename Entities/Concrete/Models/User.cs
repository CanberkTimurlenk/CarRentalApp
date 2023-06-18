using Core.Entities.Abstract;

namespace Entities.Concrete.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }

        public Customer Customer { get; set; }

        public ICollection<UserOperationClaim> UserOperationClaims { get; set; }



    }
}
