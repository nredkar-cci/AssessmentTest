using System.Linq.Expressions;
using AssessmentTest.Domain.Entities;

namespace AssessmentTest.Application.IRepository.IBaseRepository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<List<T>> GetAllAsync(CancellationToken ct = default);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

        Task<T> AddAsync(T entity, CancellationToken ct = default);

        Task<T> UpdateAsync(T entity, CancellationToken ct = default);

        Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default);
    }
}
