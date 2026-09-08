using AssessmentTest.Application.Security;
using AssessmentTest.Domain.Enums;

namespace AssessmentTest.Api.Middleware
{
    public class CurrentUser : ICurrentUser
    {
        public Guid? UserId { get; set; }

        public string? Email { get; set; }

        public Role? Role { get; set; }

        public bool IsAuthenticated => UserId is not null;
    }
}
