using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Core.Auth.Requests
{
    public record Logout(string Username, string Token) : IRequest;
}