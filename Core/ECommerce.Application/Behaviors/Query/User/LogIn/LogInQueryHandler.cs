using ECommerce.Application.Behaviors.Query.AppUser.LogIn;
using ECommerce.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace ECommerce.Application.Behaviors.Query.User.LogIn
{
	public class LogInQueryHandler : IRequestHandler<LogInQueryRequest, LogInQueryResponce>
	{
		private readonly ITokenService _tokenService;
		private readonly UserManager<Domain.Entities.Concretes.AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;


		public LogInQueryHandler(UserManager<Domain.Entities.Concretes.AppUser> userManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_tokenService = tokenService;
			_roleManager = roleManager;
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

			var roleName = await _userManager.GetRolesAsync(user);
			var token = _tokenService.CreateToken(user, roleName[0]!);
			return new LogInQueryResponce { Token = token };
		}
	}
}
