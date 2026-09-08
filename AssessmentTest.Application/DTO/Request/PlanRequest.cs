using AssessmentTest.Domain.Entities;
using AssessmentTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssessmentTest.Application.DTO.Request
{
    public class PlanRequest
    {
        [Required]
        public PlanType PlanType { get; set; }

        [Required]
        public string? PlanName { get; set; }

        [Required]
        public double Rate { get; set; }

        [Required]
        public CoachLevel CoachLevel { get; set; }
    }
}
