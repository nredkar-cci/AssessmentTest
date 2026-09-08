using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;

namespace AssessmentTest.Application.Services.AuthService
{
    public interface IAuthService
    {
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);

        Task<AuthResponse?> LoginAsync(LoginRequest request);
    }
}
