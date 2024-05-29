using ECommerce.Application.Behaviors.Query.AppUser.LogIn;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.DTOs;
using ECommerce.Domain.Entities.Concretes;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.User.LogIn
{
	public class LogInQueryHandler : IRequestHandler<LogInQueryRequest, LogInQueryResponce>
	{
		private readonly ITokenService _tokenService;
		private readonly UserManager<Domain.Entities.Concretes.AppUser> _userManager;


		public LogInQueryHandler(UserManager<Domain.Entities.Concretes.AppUser> userManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_tokenService = tokenService;
		}

		public async Task<LogInQueryResponce> Handle(LogInQueryRequest request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByNameAsync(request.UserName!);

			if (user is null)
				return new LogInQueryResponce();

			if (!user.EmailConfirmed)
				return new LogInQueryResponce() { EnailConfirmMessage = "Email is not confirmed" };

			var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password!);
			if (!checkPassword)
				return new LogInQueryResponce() { PasswordWrongMessage = "Login or password is wrong" };

			var token = _tokenService.CreateToken(user);

			return new LogInQueryResponce { Token = token };
		}
	}
}
