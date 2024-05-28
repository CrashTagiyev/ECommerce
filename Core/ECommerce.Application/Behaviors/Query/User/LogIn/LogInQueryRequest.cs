using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.AppUser.LogIn
{
	public class LogInQueryRequest:IRequest<LogInQueryResponce>
	{
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
