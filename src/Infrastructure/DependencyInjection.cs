using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Files;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Infrastructure.Communication.EmailSender;
using CleanArchitecture.Infrastructure.Communication.SmsSender;
using CleanArchitecture.Infrastructure.Communication.NotificationSender;
using DinkToPdf.Contracts;
using DinkToPdf;
using CleanArchitecture.Infrastructure.FileConverter.HtmlToPdf;
using System.IO;
using CleanArchitecture.Infrastructure.Utility;

namespace CleanArchitecture.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			/*
			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
			{
					services.AddDbContext<ApplicationDbContext>(options =>
							options.UseInMemoryDatabase("CleanArchitectureDb"));
			}
			else
			{
					services.AddDbContext<ApplicationDbContext>(options =>
							options.UseSqlServer(
									configuration.GetConnectionString("DefaultConnection"),
									b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
			}
			*/


			services.AddDbContext<ApplicationDbContext>(options =>
														options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
									b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


			services.AddScoped<IDomainEventService, DomainEventService>();

			/*
						services.AddDefaultIdentity<ApplicationUser>()
									.AddEntityFrameworkStores<ApplicationDbContext>();

						services.AddIdentityServer()
							.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
			*/

			var context = new CustomAssemblyLoadContext();
			context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), @"CustomDLL\\libwkhtmltox.dll"));

			services.AddTransient<IDateTime, DateTimeService>();
			services.AddTransient<IIdentityService, IdentityService>();
			services.AddTransient<ICommunication, ChartService>();
			services.AddTransient<ICommunication, EmailService>();
			services.AddTransient<ICommunication, SmsService>();
			services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
			services.AddTransient<IFileConverter, HtmlToPdfService>();
			services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));



			services.AddAuthentication()
					.AddIdentityServerJwt();

			services.AddSignalR();

			var emailConfig = configuration
								.GetSection("EmailConfiguration")
								.Get<EmailConfiguration>();
			services.AddSingleton(emailConfig);

			var smsConfig = configuration
								.GetSection("SmsConfiguration")
								.Get<SmsConfiguration>();
			services.AddSingleton(smsConfig);

			return services;
		}
	}
}