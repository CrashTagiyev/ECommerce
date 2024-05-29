using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Command.UserCommands
{
	public class UserRegisterCommandResponce
	{
		public string? ConfirmMessage { get; set; } = null;
		public bool IsCreated { get; set; }
	}
}
