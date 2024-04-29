using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Auth.Requests;
using Core.Auth.Responses;
using Core.Interfaces;
using Data.Auth;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Core.Auth.Handlers
{
    public class TokenHandler : IRequestHandler<Token, TokenResponse>
    {
        private readonly IAppDbContext _context;

        public TokenHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TokenResponse> Handle(Token request, CancellationToken cancellationToken)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateToken(request.UserId, request.Key);
            return await SaveTokenDetails(request.UserId, request.Username, accessToken, refreshToken, cancellationToken);
        }

        private string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            using (var cryptoProvider = RandomNumberGenerator.Create())
            {
                cryptoProvider.GetBytes(byteArray);

                return Convert.ToBase64String(byteArray);
            }
        }

        private string GenerateToken(int userId, string key)
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("uid", userId.ToString())

                }),
                Expires = DateTime.Now.AddHours(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
               SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(descriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private async Task<TokenResponse> SaveTokenDetails(int userId, string username, string token, string refreshToken, CancellationToken cancellationToken)
        {
            var userRefreshToken = new RefreshToken
            {
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                ExpirationDate = DateTime.Now.AddHours(120),
                IsArchived = false,
                RefreshKey = refreshToken,
                Token = token,
                Username = username,
                UserId = userId
            };
            await _context.RefreshTokens.AddAsync(userRefreshToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new TokenResponse { Token = token, RefreshToken = refreshToken };
        }
    }
}