using AssessmentTest.Application.Email;
using AssessmentTest.Application.IRepository.IBaseRepository;
using AssessmentTest.Application.IRepository.IClientRepository;
using AssessmentTest.Application.IRepository.IFitnessCoachRepository;
using AssessmentTest.Application.IRepository.IPlanRepository;
using AssessmentTest.Application.IRepository.IUserRepository;
using AssessmentTest.Application.Security;
using AssessmentTest.Domain.Entities;
using AssessmentTest.Infrastructure.BackgroundServices.BackgroundWorkerService;
using AssessmentTest.Infrastructure.BackgroundServices.EmailJob;
using AssessmentTest.Infrastructure.Email;
using AssessmentTest.Infrastructure.Persistence;
using AssessmentTest.Infrastructure.Persistence.Repositories.BaseRepository;
using AssessmentTest.Infrastructure.Persistence.Repositories.ClientRepository;
using AssessmentTest.Infrastructure.Persistence.Repositories.FitnessCoachRepository;
using AssessmentTest.Infrastructure.Persistence.Repositories.PlanRepository;
using AssessmentTest.Infrastructure.Persistence.Repositories.UserRepository;
using AssessmentTest.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssessmentTest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUsersRepository, UserRepository>();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IPlanRepository, PlanRepository>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFitnessCoachRepository, FitnessCoachRepository>();

            services.AddScoped<PendingEmailJob>();

            services.AddHostedService<BackgroundWorkerService>();

            return services;
        }
    }
}
