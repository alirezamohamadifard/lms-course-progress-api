using Lms.Application.Contracts.Auth;
using Lms.Application.Contracts.Courses;
using Lms.Application.Contracts.Enrollments;
using Lms.Application.Contracts.Lessons;
using Lms.Infrastructure.Authentication;
using Lms.Infrastructure.Persistence;
using Lms.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException($"Connection string as {nameof(configuration)} was not found");

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddSingleton<PasswordHasher>();
            services.AddSingleton<JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();

            return services;
        }
    }
}