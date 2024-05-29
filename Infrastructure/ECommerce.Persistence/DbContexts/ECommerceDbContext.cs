using ECommerce.Domain.Entities.Abstracts;
using ECommerce.Domain.Entities.Common;
using ECommerce.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.DbContexts;

public class ECommerceDbContext : IdentityDbContext<AppUser>
{
	public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
	{
		
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	// SaveChange
	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var datas = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);

		foreach (var item in datas)
		{
			if (item.Entity is IBaseEntity entity)
			{
				if (item.State == EntityState.Added)
					entity.CreatedAt = DateTime.Now;
				entity.UpdatedAt = DateTime.Now;
			}
		}
		return base.SaveChangesAsync(cancellationToken);
	}

	// Tables
	public virtual DbSet<Product> Products { get; set; }
	public virtual DbSet<Category> Categories { get; set; }
	public virtual DbSet<Order> Orders { get; set; }
}
