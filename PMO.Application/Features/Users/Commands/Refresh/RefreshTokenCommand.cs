using System;
using System.Collections.Generic;
using System.Text;

namespace PMO.Application.Features.Users.Commands.Refresh;

public sealed record RefreshTokenCommand : IRequest<Result<bool>>;