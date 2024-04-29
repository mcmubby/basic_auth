using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationEP.Models
{
    public record RefreshTokenRequest(string Username, string RefreshToken);
}