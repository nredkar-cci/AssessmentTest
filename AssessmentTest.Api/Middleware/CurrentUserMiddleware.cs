using System.Security.Claims;
using AssessmentTest.Domain.Enums;

namespace AssessmentTest.Api.Middleware
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CurrentUser currentUser)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                currentUser.UserId = Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId) ? userId: null;

                currentUser.Email = context.User.FindFirstValue(ClaimTypes.Email);

                currentUser.Role = Enum.TryParse(context.User.FindFirstValue(ClaimTypes.Role), out Role role) ? role: null;
            }

            await _next(context);
        }
    }
}
