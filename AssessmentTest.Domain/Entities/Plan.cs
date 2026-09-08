using AssessmentTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Domain.Entities
{
    public class Plan : BaseEntity
    {
        public PlanType PlanType { get; set; }

        public double Rate { get; set; }

        //Taking a shortcut here.  Ideally this should be done using another table where I can do 1 - Many. - A
        public CoachLevel CoachLevel { get; set; }
    }
}
