using System.ComponentModel.DataAnnotations;

namespace AssessmentTest.Application.DTO.Request
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
