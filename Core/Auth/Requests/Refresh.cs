using Core.Auth.Responses;
using MediatR;

namespace Core.Auth.Requests
{
    public record Refresh(string RefreshToken, string Username, string Key) : IRequest<TokenResponse>;
}