using Application;
using Domain.Configurations;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Kastel_Planner_Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.Configure<SmtpConfig>(Configuration.GetSection("SmtpConfig"));
            services.AddDatabase(Configuration.GetConnectionString("DefaultConnection"));
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                 


                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            services.AddRepositories();
            services.AddServices();
            services.AddControllers();
            services.AddMvc()
            .AddNewtonsoftJson();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt =>
                {
                    var jwtSecret = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
                        ValidateIssuer = false, // Development only
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // TODO: Production error page.
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=users}/{id?}");
            });
        }
    }
}
