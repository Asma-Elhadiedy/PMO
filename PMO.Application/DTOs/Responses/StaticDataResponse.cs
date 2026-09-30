using System;
using System.Collections.Generic;
using System.Text;

namespace PMO.Application.DTOs.Responses;

public sealed record StaticDataResponse(
    int Id,
    string Name);

