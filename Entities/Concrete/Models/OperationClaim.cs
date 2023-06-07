using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string OperationClaimName { get; set; }

        public ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
