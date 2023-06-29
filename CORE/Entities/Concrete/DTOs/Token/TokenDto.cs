using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete.DTOs.Token
{
    public record TokenDto
    {
        public AccessToken AccessToken { get; init; }
        public RefreshToken RefreshToken { get; init; }
    }
}
