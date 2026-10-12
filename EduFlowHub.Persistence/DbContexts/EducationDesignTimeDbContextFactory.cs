using Microsoft.EntityFrameworkCore.Design;

namespace EduFlowHub.Persistence.DbContexts;

internal class EducationDesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<EducationDbContext>
{
    public EducationDbContext CreateDbContext(string[] args)
    {
        DotNetEnv.Env.TraversePath().Load(".env.development");
        
        var host = Environment.GetEnvironmentVariable("ConnectionStrings__Postgres__DB_HOST");
        var port = Environment.GetEnvironmentVariable("ConnectionStrings__Postgres__DB_PORT");
        var db = Environment.GetEnvironmentVariable("ConnectionStrings__Postgres__DB_NAME");
        var user = Environment.GetEnvironmentVariable("ConnectionStrings__Postgres__DB_USER");
        var password = Environment.GetEnvironmentVariable("ConnectionStrings__Postgres__DB_PASSWORD");

        if (host is null ||
            port is null ||
            db is null ||
            user is null ||
            password is null)
        {
            throw new InvalidOperationException("Enviorment varibles for connection string not configured");
        }

        var connectionString =
            $"Host={host};Port={port};Database={db};Username={user};Password={password}";

        var optionsBuilder = new DbContextOptionsBuilder<EducationDbContext>()
            .UseNpgsql(connectionString);

        return new EducationDbContext(optionsBuilder.Options);
    }
}