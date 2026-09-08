using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Domain.Entities;
using AssessmentTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.Mappings
{
    public static class FitnessCoachMappings
    {
        public static FitnessCoachResponse ToResponse(this FitnessCoach fitnessCoach, User user) => new()
        {
          FitnessCoachId = fitnessCoach.Id,

          CoachName = user.Name,

          //Can be done using enum description but to save time taking a shortcut
          CoachLevel = fitnessCoach.CoachLevel == CoachLevel.Regular ? "Regular": "Elite",

          IsCertified = fitnessCoach.IsCertified,

          Expirence = fitnessCoach.ExpirenceYears
        };
    }
}
