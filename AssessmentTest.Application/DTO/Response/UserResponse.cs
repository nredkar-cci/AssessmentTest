using AssessmentTest.Domain.Enums;

namespace AssessmentTest.Application.DTO.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public Gender? Gender { get; set; }

        public Role Role { get; set; }
    }
}
