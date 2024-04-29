using Core.Auth.Responses;
using MediatR;

namespace Core.Auth.Requests
{
    public record Token(string Username, int UserId, string Key) : IRequest<TokenResponse>;
}