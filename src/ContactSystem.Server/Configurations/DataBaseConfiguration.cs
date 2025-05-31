using ContactSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Server.Configurations;

public static class DataBaseConfiguration
{
    public static void Configuration(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(connectionString));
    }
}
