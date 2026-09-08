using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.Mappings
{
    public static class PlanMappings
    {
        public static PlanResponse ToResponse(this Plan plan) => new()
        {
            PlanId = plan.Id,
            PlanType = plan.PlanType,
            PlanName = plan.PlanName,
            Rate = plan.Rate,
            CoachLevel = plan.CoachLevel,
        };

        public static Plan ToEntity(this PlanRequest request) => new()
        {
            PlanType = request.PlanType,
            PlanName = request.PlanName,
            Rate = request.Rate,
            CoachLevel = request.CoachLevel,
        };
    }
}
