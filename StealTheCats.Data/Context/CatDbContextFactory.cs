using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StealTheCats.Data.Context;

public class CatDbContextFactory : IDesignTimeDbContextFactory<CatDbContext>
{
    public CatDbContext CreateDbContext(string[] args)
    {
        // Load the configuration file
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        // Create DbContextOptions
        var optionsBuilder = new DbContextOptionsBuilder<CatDbContext>();
        var connectionString = configuration.GetConnectionString("CatDB"); 
        optionsBuilder.UseSqlServer(connectionString);

        return new CatDbContext(optionsBuilder.Options);
    }
}
