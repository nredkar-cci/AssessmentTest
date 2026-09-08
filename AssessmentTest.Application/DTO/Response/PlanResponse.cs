using AssessmentTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssessmentTest.Application.DTO.Response
{
    public class PlanResponse
    {
        public Guid PlanId { get; set; }

        public PlanType PlanType { get; set; }
       
        public double Rate { get; set; }

        public CoachLevel CoachLevel { get; set; }
    }
}
