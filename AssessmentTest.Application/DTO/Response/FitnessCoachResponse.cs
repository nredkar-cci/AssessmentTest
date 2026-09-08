using AssessmentTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.DTO.Response
{
    public class FitnessCoachResponse
    {
        public Guid FitnessCoachId { get; set; }

        public string? CoachName { get; set; }

        public string? CoachLevel { get; set; }

        public bool IsCertified { get; set; }

        public int Expirence { get; set; }
    }
}
