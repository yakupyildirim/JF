﻿using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDbContext
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTime _dateTime;
		private readonly IDomainEventService _domainEventService;

		public ApplicationDbContext(
				DbContextOptions options,
				ICurrentUserService currentUserService,
				IDomainEventService domainEventService,
				IDateTime dateTime) : base(options)
		{
			_currentUserService = currentUserService;
			_domainEventService = domainEventService;
			_dateTime = dateTime;
		}

		public DbSet<TodoItem> TodoItems { get; set; }

		public DbSet<TodoList> TodoLists { get; set; }

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = _currentUserService.UserId;
						entry.Entity.Created = _dateTime.Now;
						break;

					case EntityState.Modified:
						entry.Entity.LastModifiedBy = _currentUserService.UserId;
						entry.Entity.LastModified = _dateTime.Now;
						break;
				}
			}

			int result = await base.SaveChangesAsync(cancellationToken);

			await DispatchEvents(cancellationToken);

			return result;
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}

		private async Task DispatchEvents(CancellationToken cancellationToken)
		{
			var domainEventEntities = ChangeTracker.Entries<IHasDomainEvent>()
					.Select(x => x.Entity.DomainEvents)
					.SelectMany(x => x)
					.ToArray();

			foreach (var domainEvent in domainEventEntities)
			{
				await _domainEventService.Publish(domainEvent);
			}
		}
	}
}