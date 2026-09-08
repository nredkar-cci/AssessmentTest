using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.Services.PlanService
{
    public interface IPlanService
    {
        Task<PlanResponse?> GetByIdAsync(Guid id);

        Task<List<PlanResponse>> GetAllAsync();

        Task<PlanResponse> AddAsync(PlanRequest planRequest);

        Task<bool> RemoveAsync(Guid id);
    }
}
