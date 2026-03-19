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
            services.AddSingleton<SqliteConnection>(_ => 
            {
                var connection = new SqliteConnection("DataSource=:memory:;");
                connection.Open();

                return connection;
            });

            services.AddDbContext<DataContext>((sp, options) => 
            {
                var connection = sp.GetRequiredService<SqliteConnection>();
                options.UseSqlite(connection);
            });
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
