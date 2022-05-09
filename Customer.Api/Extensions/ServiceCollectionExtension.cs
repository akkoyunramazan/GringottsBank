using Customer.Api.Data;
using Customer.Api.Mappers;
using Customer.Api.Repositories;
using Customer.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomerFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // Service
            services.AddScoped<ICustomerService, CustomerService>();

            // Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Mappers
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            // Connection String
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

            return services;
        }
    }
}