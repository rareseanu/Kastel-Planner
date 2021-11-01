using Application.BeneficiaryWeeklyLogs;
using Application.HostedServices;
using Application.Labels;
using Application.Persons;
using Application.PersonsLabels;
using Application.PersonsRoles;
using Application.Roles;
using Application.Schedules;
using Application.TicketMessages;
using Application.Tickets;
using Application.Users;
using Application.Utils;
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
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketMessageService, TicketMessageService>();

            services.AddHostedService<ScheduleHostedService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
