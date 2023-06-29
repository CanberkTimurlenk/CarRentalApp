using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete.DTOs.Token
{
    public record RefreshToken
    {
        public string Token { get; init; }
        public DateTime Expiration { get; init; }

    }
}
