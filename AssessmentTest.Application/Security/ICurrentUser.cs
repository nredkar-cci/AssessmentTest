using AssessmentTest.Domain.Enums;

namespace AssessmentTest.Application.Security
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }

        string? Email { get; }

        Role? Role { get; }

        bool IsAuthenticated { get; }
    }
}
