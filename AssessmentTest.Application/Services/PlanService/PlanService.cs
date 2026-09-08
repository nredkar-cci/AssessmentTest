using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Application.Email;
using AssessmentTest.Application.IRepository.IPlanRepository;
using AssessmentTest.Application.IRepository.IUserRepository;
using AssessmentTest.Application.Mappings;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.Services.PlanService
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<PlanResponse> AddAsync(PlanRequest planRequest)
        {
            var created = await _planRepository.AddAsync(planRequest.ToEntity());

            return PlanMappings.ToResponse(created);
        }

        public async Task<List<PlanResponse>> GetAllAsync()
        {
            var plan = await _planRepository.GetAllAsync();
            return plan.Select(PlanMappings.ToResponse).ToList();
        }

        public async Task<PlanResponse?> GetByIdAsync(Guid id)
        {
            var plan = await _planRepository.GetByIdAsync(id);

            if (plan != null)
            { 
                return PlanMappings.ToResponse(plan);
            }
            return null;
        }
    }
}
