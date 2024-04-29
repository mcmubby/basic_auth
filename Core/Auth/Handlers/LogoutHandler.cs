using Core.Auth.Requests;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Auth.Handlers
{
    public class LogoutHandler : IRequestHandler<Logout>
    {
        private readonly IAppDbContext _context;

        public LogoutHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(Logout request, CancellationToken cancellationToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Username == request.Username && r.Token == request.Token && r.IsArchived == false) ?? throw new NotFoundException();

            token.IsArchived = true;
            token.DateUpdated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}