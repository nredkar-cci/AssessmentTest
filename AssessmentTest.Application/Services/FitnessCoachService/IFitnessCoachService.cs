using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.Services.FitnessCoachService
{
    public interface IFitnessCoachService
    {
        public Task<FitnessCoachResponse?> ConvertToCoachAsync(FitnessCoachRequest fitnessCoachRequest, Guid currentUserId);

        Task<FitnessCoachResponse?> GetByIdAsync(Guid id);

        Task<List<FitnessCoachResponse>> GetAllAsync();
    }
}
