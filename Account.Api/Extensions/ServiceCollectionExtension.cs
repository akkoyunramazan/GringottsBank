using Account.Api.Data;
using Account.Api.Mappers;
using Account.Api.Repositories;
using Account.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAccountFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // Service
            services.AddScoped<IAccountService, AccountService>();

            // Repository
            services.AddScoped<IAccountRepository, AccountRepository>();

            // Mappers
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            // Connection String
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

            return services;
        }
    }
}