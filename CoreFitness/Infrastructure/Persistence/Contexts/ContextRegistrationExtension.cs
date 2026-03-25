using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public static class ContextRegistrationExtension
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env) 
    {

        if (env.IsDevelopment())
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException("DefaultConnection not found"); 

            services.AddDbContext<DataContext>(options => 
                options.UseSqlServer(connectionString));
        }
        else 
        {
            Console.WriteLine("Production Enviroment");

            services.AddDbContext<DataContext>((sp, options) =>
            {
                var connection = configuration.GetConnectionString("ProductionDatabaseUri")
                    ?? throw new ArgumentException("Production Database Uri not Provided");

                options.UseSqlite(connection);
            });

        }
            return services;
    }
}
