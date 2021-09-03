using Application.RepositoryInterfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<KastelPlannerDbContext>(o =>
            {
                o.UseNpgsql(connectionString);
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IBeneficiaryWeeklyLogRepository, BeneficiaryWeeklyLogRepository>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddScoped<IPersonLabelRepository, PersonLabelRepository>();
            services.AddScoped<IPersonRoleRepository, PersonRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
        }
    }
}
