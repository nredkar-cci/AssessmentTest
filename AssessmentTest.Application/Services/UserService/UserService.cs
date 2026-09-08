using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Application.Email;
using AssessmentTest.Application.IRepository.IUserRepository;
using AssessmentTest.Application.Mappings;
using AssessmentTest.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace AssessmentTest.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMemoryCache _memoryCache;

        private readonly IEmailService _emailService;

        private const string allUsersKey = "users:all";

        public UserService(IUsersRepository userRepository, IMemoryCache memoryCache, IEmailService emailService)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
            _emailService = emailService;
        }

        public async Task<UserResponse?> GetByIdAsync(Guid id)
        {

            var user = await _userRepository.GetByIdAsync(id);
            return user?.ToResponse();
        }

        public async Task<List<UserResponse>> GetAllAsync() =>
        (await _memoryCache.GetOrCreateAsync(allUsersKey, async entry =>
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(UserMappings.ToResponse).ToList();
        }))!;

        public async Task<UserResponse> AddAsync(UserRequest userRequest)
        {
            var created = await _userRepository.AddAsync(userRequest.ToEntity());
            _memoryCache.Remove(allUsersKey);

            return created.ToResponse();
        }

        #region Private Methods
        private Task QueueWelcomeEmail(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return Task.CompletedTask;
            }

            var name = WebUtility.HtmlEncode(user.Name ?? "there");

            var body = $"""
                <p>Hi {name},</p>
                <p>Your account has been created. You are signed up as <strong>{user.Role}</strong>.</p>
                <p>Welcome aboard.</p>
                """;

            return _emailService.QueueEmail(new EmailMessage
            {
                Recipient = user.Email,
                Subject = "Your account has been created",
                Body = body
            });
        }
        #endregion
    }
}
