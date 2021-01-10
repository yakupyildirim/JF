using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddIdentityServerConfig(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.Password.RequiredLength = 0;
				options.Password.RequiredUniqueChars = 0;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+'#!/^%{}*";
			}).AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddIdentityServer()
							.AddDeveloperSigningCredential()
							.AddOperationalStore(options =>
							{
								options.ConfigureDbContext = builder => builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
								options.EnableTokenCleanup = true;
							})
							 .AddConfigurationStore(options =>
							 {
								 options.ConfigureDbContext = builder => builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
							 })
							.AddAspNetIdentity<ApplicationUser>();
			return services;
		}

		public static IServiceCollection AddServices<TUser>(this IServiceCollection services) where TUser : IdentityUser<string>, new()
		{
			// services.AddTransient<IProfileService, IdentityClaimsProfileService>();
			return services;
		}

		public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, string connectionString)
		{
			//services.AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(connectionString));
			//services.AddDbContext<AppPersistedGrantDbContext>(options => options.UseNpgsql(connectionString));
			services.AddDbContext<AppConfigurationDbContext>(options => options.UseNpgsql(connectionString));
			return services;
		}
	}
}