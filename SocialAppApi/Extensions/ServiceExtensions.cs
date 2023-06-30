using Microsoft.EntityFrameworkCore;
using SocialAppApi.Data;
using SocialAppApi.Interfaces;
using SocialAppApi.Services;

namespace SocialAppApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) 
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("sqlConnection"));
            });

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
