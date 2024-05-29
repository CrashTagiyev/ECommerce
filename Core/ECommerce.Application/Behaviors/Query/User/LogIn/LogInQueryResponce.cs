using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.AppUser.LogIn
{
	public class LogInQueryResponce
	{
		public string? Token { get; set; } = null;

		public string? EnailConfirmMessage { get; set; } = null;
		public string? PasswordWrongMessage { get; set; } = null;
	}
}
