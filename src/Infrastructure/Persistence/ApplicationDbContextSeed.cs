using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;

namespace CleanArchitecture.Infrastructure.Persistence
{
	public static class ApplicationDbContextSeed
	{
		/*
		public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
		{
			var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

			if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
			{
				await userManager.CreateAsync(defaultUser, "Administrator1!");
			}
		}
		*/

		public static void EnsureSeedData(IServiceProvider provider)
		{
			var configuration = provider.GetRequiredService<IConfiguration>();
			//provider.GetRequiredService<AppIdentityDbContext>().Database.Migrate();
			//provider.GetRequiredService<AppPersistedGrantDbContext>().Database.Migrate();
			provider.GetRequiredService<AppConfigurationDbContext>().Database.Migrate();

			var context = provider.GetRequiredService<AppConfigurationDbContext>();
			if (!context.Clients.Any())
			{
				var clients = new List<Client>();
				configuration.GetSection("IdentityServer:Clients").Bind(clients);
				foreach (var client in clients)
					context.Clients.Add(client.ToEntity());
				context.SaveChanges();
			}
			if (!context.ApiResources.Any())
			{
				var apiResources = new List<ApiResource>();
				configuration.GetSection("IdentityServer:ApiResources").Bind(apiResources);
				foreach (var apiResource in apiResources)
					context.ApiResources.Add(apiResource.ToEntity());
				context.SaveChanges();
			}
			if (!context.IdentityResources.Any())
			{
				var identityResources = new List<IdentityResource>();
				configuration.GetSection("IdentityServer:IdentityResources").Bind(identityResources);
				foreach (var identityResource in identityResources)
					context.IdentityResources.Add(identityResource.ToEntity());
				context.SaveChanges();
			}
		}

		/*
				public static async Task SeedSampleDataAsync(ApplicationDbContext context)
				{
					// Seed, if necessary
					if (!context.TodoLists.Any())
					{
						context.TodoLists.Add(new TodoList
						{
							Title = "Shopping",
							Items =
												{
														new TodoItem { Title = "Apples", Done = true },
														new TodoItem { Title = "Milk", Done = true },
														new TodoItem { Title = "Bread", Done = true },
														new TodoItem { Title = "Toilet paper" },
														new TodoItem { Title = "Pasta" },
														new TodoItem { Title = "Tissues" },
														new TodoItem { Title = "Tuna" },
														new TodoItem { Title = "Water" }
												}
						});

						await context.SaveChangesAsync();
					}
				}
				*/
	}
}
