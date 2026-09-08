using AssessmentTest.Domain.Enums;

namespace AssessmentTest.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public Gender? Gender { get; set; }

        public Role Role { get; set; }

        public string PasswordHash { get; set; } = null!;
    }
}
