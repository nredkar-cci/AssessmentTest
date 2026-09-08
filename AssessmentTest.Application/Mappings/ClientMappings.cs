using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.Mappings
{
    public static class ClientMappings
    {
        public static ClientResponse ToResponse(this Client client, User user, User coach, Plan plan ) => new()
        {
            ClientId = client.Id,
            CoachName = coach.Name,
            ClientName = user.Name,
            PlanStatus = client.PlanStatus,
            ExpiryDate = client.PlanExpiry,
            PlanRate = plan.Rate,
            PlanType = plan.PlanName
        };
    }
}
