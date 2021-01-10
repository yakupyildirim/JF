using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanArchitecture.Infrastructure.Persistence
{
	public class AppConfigurationDbContext : ConfigurationDbContext
	{
		public AppConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}

	public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<AppConfigurationDbContext>
	{
		public AppConfigurationDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
			var storeOptions = new ConfigurationStoreOptions();
			var connectionString = "User ID=postgres;Password=Postgre218.;Server=localhost;Port=5432;Database=JF;Integrated Security=true;Pooling=true;";
			builder.UseNpgsql(connectionString);
			return new AppConfigurationDbContext(builder.Options, storeOptions);
		}
	}
}