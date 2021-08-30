using Infrastructure.Context;
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
    }
}
