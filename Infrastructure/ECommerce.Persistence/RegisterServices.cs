using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Persistence.DbContexts;
using ECommerce.Persistence.Repositories;
using ECommerce.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence;

public static class RegisterServices
{
	public static void AddPersistenceRegister(this IServiceCollection services)
	{
		services.AddDbContext<ECommerceDbContext>(options =>
		{
			ConfigurationBuilder configurationBuilder = new();
			var builder = configurationBuilder.AddJsonFile("appsettings.json").Build();

			options.UseLazyLoadingProxies()
				   .UseSqlServer(builder.GetConnectionString("default"));
		});

		services.AddIdentityCore<AppUser>()
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<ECommerceDbContext>().AddDefaultTokenProviders();

		// Register all Repository in Persistence

		// All Read Repository
		services.AddScoped<IReadOrderRepository, ReadOrderRepository>();
		services.AddScoped<IReadProductRepository, ReadProductRepository>();
		services.AddScoped<IReadCategoryRepository, ReadCategoryRepository>();

		// All Write Repository
		services.AddScoped<IWriteOrderRepository, WriteOrderRepository>();
		services.AddScoped<IWriteProductRepository, WriteProductRepository>();
		services.AddScoped<IWriteCategoryRepository, WriteCategoryRepository>();



		// All Services Register
		services.AddScoped<IProductService, ProductService>();
		services.AddScoped<ICategoryService, CategoryService>();
	}
}
