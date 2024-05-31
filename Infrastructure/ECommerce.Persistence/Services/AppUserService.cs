using Azure.Core;
using ECommerce.Application.Repositories.AppUserAbstractRepo;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Services
{
	public class AppUserService : IAppUserService
	{
		private readonly IReadAppUserRepository _appUserReadRepository;
		private readonly UserManager<AppUser>  _userManager;

		public AppUserService(IReadAppUserRepository appUserReadRepository, UserManager<AppUser> userManager)
		{
			_appUserReadRepository = appUserReadRepository;
			_userManager = userManager;
		}

		public async Task<List<AppUser>> GetAllAsync()
		{
			var users=	await _appUserReadRepository.GetAllUsersAsync();
			return users.ToList();
		}

		public async Task<AppUser> FindUserByTokenAsync(string token)
		{
			var users = await GetAllAsync();

			foreach (var userI in users)
			{
				var isValid = await _userManager.VerifyUserTokenAsync(userI, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
				if (isValid)
					return userI;
			}
			return null!;
		}

		public async Task<IdentityResult> ResetPasswordAsync(AppUser appUser, string token, string newPassword)
		{
			var result =await _userManager.ResetPasswordAsync(appUser, token, newPassword);
			return result;
		}
	}
}
