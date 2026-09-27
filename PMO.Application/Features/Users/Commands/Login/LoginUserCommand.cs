
namespace PMO.Application.Features.Users.Commands.Login;

public sealed record LoginUserCommand
(
    [property: DefaultValue ("asma@app.com")] string Email,
    [property: DefaultValue ("123456")] string Password
) : IRequest<Result<LoginUserResponse>>;
