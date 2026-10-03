
namespace PMO.Application.DTOs.Responses;

public sealed record LoginUserResponse(
    string Token,
    //string RefreshToken,
    DateTime Expiration);

