using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Application.IRepository.IUserRepository;
using AssessmentTest.Application.Security;
using AssessmentTest.Domain.Entities;
using AssessmentTest.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AssessmentTest.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(
            IUsersRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.GetByEmailAsync(request.Email) is not null)
                return null;

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Gender = request.Gender,
                Role = Role.User
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var created = await _userRepository.AddAsync(user);
            return Issue(created);
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null || !user.IsActive)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            return Issue(user);
        }

        private AuthResponse Issue(User user)
        {
            var (token, expiresAt) = _tokenGenerator.Create(user);
            return new AuthResponse { AccessToken = token, ExpiresAt = expiresAt };
        }
    }
}
