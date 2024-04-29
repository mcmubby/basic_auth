using Core.Auth.Requests;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Auth.Handlers
{
    public class LoginHandler : IRequestHandler<Login, (bool,int)>
    {
        private readonly IAppDbContext _context;

        public LoginHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool,int)> Handle(Login request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstAsync(u => u.Username == request.Username.ToLower().Trim()) ?? throw new NotFoundException();

            var validPassword = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash);

            return (validPassword, user.Id);
        }
    }
}