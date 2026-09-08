using AssessmentTest.Application.IRepository.IBaseRepository;
using AssessmentTest.Application.IRepository.IPlanRepository;
using AssessmentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Infrastructure.Persistence.Repositories.PlanRepository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly IBaseRepository<Plan> _baseRepository;

        public PlanRepository(IBaseRepository<Plan> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<Plan?> GetByIdAsync(Guid id) => _baseRepository.GetByIdAsync(id);

        public Task<List<Plan>> GetAllAsync() => _baseRepository.GetAllAsync();

        public Task<Plan> AddAsync(Plan plan) => _baseRepository.AddAsync(plan);

        public Task<bool> RemoveAsync(Guid id) => _baseRepository.SoftDeleteAsync(id);
    }
}
