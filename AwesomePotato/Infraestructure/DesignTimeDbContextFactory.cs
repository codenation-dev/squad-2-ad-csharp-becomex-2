using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AwesomePotato.Infraestructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                        .SetBasePath(Directory.GetCurrentDirectory())
                                                        .AddJsonFile("appsettings.json")
                                                        .Build();

            var builder = new DbContextOptionsBuilder<IdentityContext>();
            var connectionString = configuration.GetConnectionString("default");
            builder.UseSqlServer(connectionString);
            return new IdentityContext(builder.Options);
        }
    }
}
