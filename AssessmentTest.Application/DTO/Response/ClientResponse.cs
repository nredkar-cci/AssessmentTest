using AssessmentTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentTest.Application.DTO.Response
{
    public class ClientResponse
    {
       public Guid ClientId {  get; set; }

       public string? ClientName { get; set; }

       public string? PlanType { get; set; }

       public double? PlanRate { get; set; }

       public string? CoachName { get; set; }

       public DateTime? ExpiryDate { get; set; }

       public PlanStatus PlanStatus { get; set; }
    }
}
