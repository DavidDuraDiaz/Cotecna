using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CotecnaB.Persistance.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CotecnaEFContext>
    {
        public CotecnaEFContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../CotectaB.WebApi/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<CotecnaEFContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString);
            return new CotecnaEFContext(builder.Options);
        }
    }
}
