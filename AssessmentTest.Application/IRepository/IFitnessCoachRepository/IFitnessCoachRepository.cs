using AssessmentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.IRepository.IFitnessCoachRepository
{
    public interface IFitnessCoachRepository
    {
        Task<FitnessCoach?> GetByIdAsync(Guid id);

        Task<List<FitnessCoach>> GetAllAsync();

        Task<FitnessCoach> AddAsync(FitnessCoach fitnessCoach);
    }
}
