using AssessmentTest.Api.Middleware;
using AssessmentTest.Application;
using AssessmentTest.Application.Security;
using AssessmentTest.Infrastructure;

namespace AssessmentTest.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI();
            services.AddInfrastructureDI(configuration);

            // Both registrations must resolve the same instance: the middleware
            // writes through CurrentUser, everything else reads through ICurrentUser.
            services.AddScoped<CurrentUser>();
            services.AddScoped<ICurrentUser>(sp => sp.GetRequiredService<CurrentUser>());

            return services;
        }
    }
}
