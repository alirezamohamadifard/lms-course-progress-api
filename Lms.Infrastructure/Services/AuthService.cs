using Lms.Application.Contracts.Auth;
using Lms.Domain.Entity;
using Lms.Domain.Enums;
using Lms.Infrastructure.Authentication;
using Lms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Infrastructure.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext appDbContext, PasswordHasher passwordHasher, JwtTokenGenerator jwtTokenGenerator)
        {
            _dbContext = appDbContext;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var emailexist = await _dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);

            if (emailexist)
                throw new InvalidOperationException("A user with this email already exists.");
            
            var paswwordHash = _passwordHasher.Hash(request.Password);

            var user = new User(
                request.FullName,
                email,
                paswwordHash,
                UserRole.Student);

            _dbContext.Add(user);
            
            await _dbContext.SaveChangesAsync();

            return CreateAuthResponse(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var user = await _dbContext.Users
                .SingleOrDefaultAsync(x => x.Email == email, cancellationToken);

            if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return CreateAuthResponse(user);
        }

        private AuthResponse CreateAuthResponse(User user) 
        {
            var token = _jwtTokenGenerator.Generator(user);

            return new AuthResponse(
                user.Id,
                user.FullName,
                user.Email,
                user.Role.ToString(),
                token);
        }
    }
}
