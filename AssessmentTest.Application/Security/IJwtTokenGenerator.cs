using AssessmentTest.Domain.Entities;

namespace AssessmentTest.Application.Security
{
    public interface IJwtTokenGenerator
    {
        (string Token, DateTime ExpiresAt) Create(User user);
    }
}
