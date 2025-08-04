using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.DbContexts;

public class MyDbContext(IConfiguration _config) : DbContext
{
    private readonly string dbConnection = _config.GetConnectionString("DefaultConnection") ?? "";

    public DbSet<Employee> Employees { get; set; } = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        optionsBuilder.EnableDetailedErrors();

        optionsBuilder.UseNpgsql(dbConnection, options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        })
        .UseSnakeCaseNamingConvention();

        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}
