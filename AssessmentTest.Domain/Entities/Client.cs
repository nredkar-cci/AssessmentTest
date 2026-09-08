using AssessmentTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Domain.Entities
{
    public class Client : BaseEntity
    {
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public PlanStatus PlanStatus { get; set; } = PlanStatus.NotStarted;

        public Guid? FitnessCoachId { get; set; }

        public FitnessCoach Coach { get; set; } = null!;

        public Guid? PlanId { get; set; }

        public Plan Plan { get; set; } = null!;

        public DateTime? PlanExpiry { get; set; }

    }
}
