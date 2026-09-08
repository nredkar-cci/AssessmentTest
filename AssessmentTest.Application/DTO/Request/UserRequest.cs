using System.ComponentModel.DataAnnotations;
using AssessmentTest.Domain.Enums;

namespace AssessmentTest.Application.DTO.Request
{
    public class UserRequest
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        [Required, EmailAddress, StringLength(256)]
        public string Email { get; set; } = null!;

        [Phone]
        public string? Phone { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }

        [Required, EnumDataType(typeof(Role))]
        public Role Role { get; set; }

        [Required, StringLength(128, MinimumLength = 8)]
        public string Password { get; set; } = null!;
    }
}
