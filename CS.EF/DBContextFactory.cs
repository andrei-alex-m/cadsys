using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CS.EF
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CadSysContext>
    {
        public CadSysContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<CadSysContext>()
                .EnableSensitiveDataLogging();

            var connectionString = configuration.GetConnectionString("con");

            dbContextBuilder.UseMySql(connectionString);

            return new CadSysContext(dbContextBuilder.Options);
        }
    }
}
