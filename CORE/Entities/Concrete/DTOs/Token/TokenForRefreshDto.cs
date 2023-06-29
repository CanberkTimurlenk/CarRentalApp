namespace Core.Entities.Concrete.DTOs.Token
{
    public record TokenForRefreshDto
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }

    }
}