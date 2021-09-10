using Application.BeneficiaryWeeklyLogs;
using Application.Labels;
using Application.Persons;
using Application.PersonsLabels;
using Application.PersonsRoles;
using Application.Roles;
using Application.Schedules;
using Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBeneficiaryWeeklyLogService, BeneficiaryWeeklyLogService>();
            services.AddScoped<ILabelService, LabelService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonsLabelsService, PersonsLabelsService>();
            services.AddScoped<IPersonsRolesService, PersonsRolesService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IScheduleService, ScheduleService>();
        }
    }
}
