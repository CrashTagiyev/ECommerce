using MediatR;

namespace ECommerce.Application.Behaviors.Query.AppUser.LogIn
{
	public class LogInQueryRequest:IRequest<LogInQueryResponce>
	{
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
