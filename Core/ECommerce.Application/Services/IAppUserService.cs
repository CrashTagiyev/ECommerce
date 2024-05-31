using Azure.Core;
using ECommerce.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
	public interface IAppUserService
	{
		Task<List<AppUser>> GetAllAsync();
		Task<AppUser> FindUserByTokenAsync(string token);
		Task<IdentityResult> ResetPasswordAsync(AppUser appUser,string token, string newPassword);

	}
}
