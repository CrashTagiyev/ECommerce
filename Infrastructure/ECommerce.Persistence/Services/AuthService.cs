using ECommerce.Application.Services;
using ECommerce.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Services
{
	public class AuthService : IAuthService
	{
		public Task LogIn(LoginDTO loginDTO)
		{
			throw new NotImplementedException();
		}

		public Task Registration(AppUserDTO appUserDTO)
		{
			throw new NotImplementedException();
		}
	}
}
