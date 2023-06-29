using Core.Entities;
using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.User
{
    public record UserForManipulationDto : IDto
    {        
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public byte[] PasswordSalt { get; init; }
        public byte[] PasswordHash { get; init; }
        public bool Status { get; init; }

    }
}
