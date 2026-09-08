using AssessmentTest.Application.DTO.Request;
using AssessmentTest.Application.DTO.Response;
using AssessmentTest.Domain.Entities;

namespace AssessmentTest.Application.Mappings
{
    public static class UserMappings
    {
        public static UserResponse ToResponse(this User user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Gender = user.Gender,
            Role = user.Role
        };

        public static User ToEntity(this UserRequest request) => new()
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Gender = request.Gender,
            Role = request.Role
        };
    }
}
