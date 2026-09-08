using AssessmentTest.Application.IRepository.IBaseRepository;
using AssessmentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Infrastructure.Persistence.Repositories.FitnessCoachRepository
{
    public class FitnessCoachRepository
    {
        private readonly IBaseRepository<FitnessCoach> _baseRepository;

        public FitnessCoachRepository(IBaseRepository<FitnessCoach> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<FitnessCoach?> GetByIdAsync(Guid id) => _baseRepository.GetByIdAsync(id);

        public Task<List<FitnessCoach>> GetAllAsync() => _baseRepository.GetAllAsync();

        public Task<FitnessCoach> AddAsync(FitnessCoach fitnessCoach) => _baseRepository.AddAsync(fitnessCoach);
    }
}
