using AssessmentTest.Application.Services.AuthService;
using AssessmentTest.Application.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace AssessmentTest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
