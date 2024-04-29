using MediatR;

namespace Core.Users.Requests
{
    public record CreateUser(string Username, string Password) : IRequest;
}