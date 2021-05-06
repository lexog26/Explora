using Explora.DataLayer.EntityConfigs;
using Explora.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Explora.DataLayer.Context
{
    public partial class ExploraContext : DbContext, IDesignTimeDbContextFactory<ExploraContext>
    {
        private IConfiguration _configuration;

        public ExploraContext(DbContextOptions<ExploraContext> options) : base(options)
        { }

        public ExploraContext() : base()
        { }

        #region DbSets

        public DbSet<ExploraFile> File { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<ExploraCollection> Collection { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new UserConfig(builder.Entity<User>());
            new ExploraFileConfig(builder.Entity<ExploraFile>());
            new ExploraCollectionConfig(builder.Entity<ExploraCollection>());

        }

        public ExploraContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExploraContext>();
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables();

            _configuration = builder.Build();
            //optionsBuilder.UseMySql(_configuration.GetConnectionString("(default)"));
            optionsBuilder.UseMySql(_configuration.GetConnectionString("ExploraConnectionString"));
            
            return new ExploraContext(optionsBuilder.Options);
        }


    }
}
