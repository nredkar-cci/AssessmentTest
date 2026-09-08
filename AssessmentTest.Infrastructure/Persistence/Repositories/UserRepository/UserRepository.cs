using AssessmentTest.Application.IRepository.IBaseRepository;
using AssessmentTest.Application.IRepository.IUserRepository;
using AssessmentTest.Domain.Entities;

namespace AssessmentTest.Infrastructure.Persistence.Repositories.UserRepository
{
    public class UserRepository : IUsersRepository
    {
        private readonly IBaseRepository<User> _baseRepository;

        public UserRepository(IBaseRepository<User> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<User?> GetByIdAsync(Guid id) => _baseRepository.GetByIdAsync(id);

        public Task<List<User>> GetAllAsync() => _baseRepository.GetAllAsync();

        public Task<User> AddAsync(User user) => _baseRepository.AddAsync(user);

        public async Task<User?> GetByEmailAsync(string email) =>
            (await _baseRepository.FindAsync(u => u.Email == email)).FirstOrDefault();
    }
}
