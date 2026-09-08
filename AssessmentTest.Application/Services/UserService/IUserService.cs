using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;

namespace AssessmentTest.Application.Services.UserService
{
    public interface IUserService
    {
        Task<UserResponse?> GetByIdAsync(Guid id);

        Task<List<UserResponse>> GetAllAsync();

        Task<UserResponse> AddAsync(UserRequest userRequest);
    }
}
