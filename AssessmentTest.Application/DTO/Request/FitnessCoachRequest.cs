using AssessmentTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssessmentTest.Application.DTO.Request
{
    public class FitnessCoachRequest
    {
        //If user Id has not been set than userId of the logged in user will be assigned
        public Guid? UserId { get; set; }

        [Required]
        public int Expirence { get; set; }

        [Required]
        public bool IsCertified { get; set; }

        [Required]
        public CoachLevel CoachLevel { get; set; }
    }
}
