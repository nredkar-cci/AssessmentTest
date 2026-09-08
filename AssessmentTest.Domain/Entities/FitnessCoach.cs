using AssessmentTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Domain.Entities
{
    public class FitnessCoach : BaseEntity
    {
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        //Second half of shortcut is here
        public CoachLevel CoachLevel { get; set; }

        public bool IsCertified { get; set; }

        public int ExpirenceYears { get; set; }
    }
}
