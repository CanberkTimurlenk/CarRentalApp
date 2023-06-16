using Core.Entities;

namespace Entities.Concrete.DTOs.User
{
    public record UserDto : IDto
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public byte[] PasswordSalt { get; init; }
        public byte[] PasswordHash { get; init; }
        public bool Status { get; init; }

    }
}
