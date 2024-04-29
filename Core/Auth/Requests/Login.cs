using MediatR;

namespace Core.Auth.Requests
{
    public record Login(string Username, string Password) : IRequest<(bool,int)>;
}