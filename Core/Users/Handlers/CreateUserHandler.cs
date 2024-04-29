using Core.Exceptions;
using Core.Interfaces;
using Core.Users.Requests;
using Data;
using MediatR;

namespace Core.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser>
    {
        private readonly IAppDbContext _context;

        public CreateUserHandler(IAppDbContext context)
        {
            _context = context;
        }

        public Task Handle(CreateUser request, CancellationToken cancellationToken)
        {
            if(_context.Users.Any(u => u.Username == request.Username)){ throw new ExistingRecordException(); }

            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password, 15);

            var user = new User
            {
                Username = request.Username.ToLower().Trim(),
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}