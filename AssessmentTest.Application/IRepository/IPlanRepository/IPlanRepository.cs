using AssessmentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.IRepository.IPlanRepository
{
    public interface IPlanRepository
    {
        Task<Plan?> GetByIdAsync(Guid id);

        Task<List<Plan>> GetAllAsync();

        Task<Plan> AddAsync(Plan plan);
    }
}
