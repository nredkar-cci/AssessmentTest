using AssessmentTest.Domain.Entities;

namespace AssessmentTest.Application.IRepository.IUserRepository
{
    public interface IUsersRepository
    {
        Task<User?> GetByIdAsync(Guid id);

        Task<List<User>> GetAllAsync();

        Task<User> AddAsync(User user);

        Task<User?> GetByEmailAsync(string email);
    }
}
