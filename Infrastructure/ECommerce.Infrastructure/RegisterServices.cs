using ECommerce.Application.Services;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce.Infrastructure;


public static class RegisterServices
{
	public static void AddInfrastructureRegister(this WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<ITokenService, TokenService>();
		builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
		builder.Services.AddScoped<IUrlHelper>(x => {
			var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
			return new UrlHelper(actionContext);
		});

		// Add Auth JWT
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
					ValidIssuer = builder.Configuration["JWT:Issuer"],
					ValidAudience = builder.Configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
				};
			});

		builder.Services.AddTransient<IEmailService, EmailService>();

	}
}
