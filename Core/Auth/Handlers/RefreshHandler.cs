using Core.Auth.Requests;
using Core.Auth.Responses;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Auth.Handlers
{
    public class RefreshHandler : IRequestHandler<Refresh, TokenResponse>
    {
        private readonly IAppDbContext _context;
        private readonly ISender _sender;

        public RefreshHandler(IAppDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

        public async Task<TokenResponse> Handle(Refresh request, CancellationToken cancellationToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Username == request.Username && r.RefreshKey == request.RefreshToken && r.IsArchived == false) ?? throw new NotFoundException();

            token.IsArchived = true;
            token.DateUpdated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return await _sender.Send(new Token(request.Username, token.UserId, request.Key));
        }
    }
}