using ECommerce.Application.Services;
using ECommerce.Domain.Entities.Concretes;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Command.UserCommands
{
	public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommandRequest, UserRegisterCommandResponce>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailService _emailService;

		public UserRegisterCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_emailService = emailService;
		}

		public async Task<UserRegisterCommandResponce> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
		{
			return	await SignUp(request);
		}


		public async Task<UserRegisterCommandResponce> SignUp(UserRegisterCommandRequest model)
		{
			try
			{
				var userExist = await _userManager.FindByEmailAsync(model.Email);
				if (userExist != null)
				{
					var responce = new UserRegisterCommandResponce
					{
						IsCreated = false,
						ConfirmMessage = "User with this email is already exist"
					};
					return responce;
				}
				else
				{
					var user = new AppUser
					{
						UserName = model.UserName,
						Email = model.Email,
						
					};
					if (!await _roleManager.RoleExistsAsync("Admin"))
					{
						var role = new IdentityRole { Name = "Admin" };
						await _roleManager.CreateAsync(role);
					}
					await _userManager.CreateAsync(user, model.Password);
					await _userManager.AddToRoleAsync(user,"Admin");
					await _emailService.SendConfirmationEmail(model.Email, user);
					var responce = new UserRegisterCommandResponce
					{
						IsCreated = true,
						ConfirmMessage = "Account created successfully.Please confirm registration from the email"
					};
					return responce;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}



	}
}
