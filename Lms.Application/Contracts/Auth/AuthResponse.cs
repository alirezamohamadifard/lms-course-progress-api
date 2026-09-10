using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Application.Contracts.Auth
{
    public sealed record AuthResponse(
    Guid UserId,
    string FullName,
    string Email,
    string Role,
    string AccessToken);
}
