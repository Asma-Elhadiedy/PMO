
using System.ComponentModel;

namespace PMO.Application.Features.Users.Commands.Register;

public sealed record RegisterUserCommand
(
    [property: DefaultValue("Asma")] string FirstName,
    [property: DefaultValue("Elh")] string LastName,
    [property: DefaultValue("asma@app.com")] string Email,
    [property: DefaultValue("123456")] string Password
) : IRequest<Result<bool>>;