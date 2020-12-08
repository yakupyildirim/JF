using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace WebUI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddControllers();

			services.AddApplication();
			services.AddInfrastructure(Configuration);

			services.AddSingleton<ICurrentUserService, CurrentUserService>();

			services.AddHttpContextAccessor();

			//services.AddHealthChecks()
			//		.AddDbContextCheck<ApplicationDbContext>();

			/*
	services.AddControllers(options =>
			options.Filters.Add(new ApiExceptionFilterAttribute()))
					.AddFluentValidation();
		*/
			//services.AddRazorPages();

			// Customise default API behaviour
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			services.AddOpenApiDocument(configure =>
      {
				configure.Title = "CleanArchitecture API";
				configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
				{
					Type = OpenApiSecuritySchemeType.ApiKey,
					Name = "Authorization",
					In = OpenApiSecurityApiKeyLocation.Header,
					Description = "Type into the textbox: Bearer {your JWT token}."
				});

				configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
      });

			// In production, the Angular files will be served from this directory
			/*
services.AddSpaStaticFiles(configuration =>
{
	configuration.RootPath = "ClientApp/dist";
});


			*/
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwaggerUi3(settings =>
			{
				settings.Path = "/api";
				settings.DocumentPath = "/api/specification.json";
			});

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseAuthentication();
			app.UseIdentityServer();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
									name: "default",
									pattern: "{controller}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});


		}
	}
}
