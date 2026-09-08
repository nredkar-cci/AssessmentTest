using AssessmentTest.Application.Services.AuthService;
using AssessmentTest.Application.Services.ClientService;
using AssessmentTest.Application.Services.FitnessCoachService;
using AssessmentTest.Application.Services.PlanService;
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
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IFitnessCoachService, FitnessCoachService>();
            return services;
        }
    }
}
