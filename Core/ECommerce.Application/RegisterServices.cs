using ECommerce.Application.Behaviors.Query.ResetPasswordQuerries;
using ECommerce.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.Application;

public static class RegisterServices
{
    public static void AddApplicationRegister(this IServiceCollection services)
    {
        services.AddMediatR(p=>p.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


	}
}
