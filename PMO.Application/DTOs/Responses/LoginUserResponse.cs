
namespace PMO.Application.DTOs.Responses;

public sealed record LoginUserResponse(string Token, DateTime Expiration )
{
    //public string RefreshToken { get; init; }
}
