using System.Linq.Expressions;
using AssessmentTest.Application.IRepository.IBaseRepository;
using AssessmentTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssessmentTest.Infrastructure.Persistence.Repositories.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<T> Set;

        public BaseRepository(AppDbContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }

        // All reads exclude soft-deleted rows.
        protected IQueryable<T> Query => Set.Where(e => !e.IsDeleted);

        public Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            Query.FirstOrDefaultAsync(e => e.Id == id, ct);

        public Task<List<T>> GetAllAsync(CancellationToken ct = default) =>
            Query.ToListAsync(ct);

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default) =>
            Query.Where(predicate).ToListAsync(ct);

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            Set.Add(entity);
            await Context.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken ct = default)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            Set.Update(entity);

            Context.Entry(entity).Property(e => e.CreatedDate).IsModified = false;

            await Context.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await GetByIdAsync(id, ct);
            if (entity is null) return false;

            entity.IsDeleted = true;
            entity.IsActive = false;
            entity.UpdatedDate = DateTime.UtcNow;
            await Context.SaveChangesAsync(ct);
            return true;
        }
    }
}
