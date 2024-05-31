using ECommerce.Domain.Entities.Concretes;
using MediatR;
using Microsoft.AspNetCore.Identity;
namespace ECommerce.Application.Behaviors.Command.ForgotPasswordCommands
{
	public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, ForgotPasswordCommandResponse>
	{

		private readonly UserManager<AppUser> _userManager;

		public ForgotPasswordCommandHandler(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		async Task<ForgotPasswordCommandResponse> IRequestHandler<ForgotPasswordCommandRequest, ForgotPasswordCommandResponse>.Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email!);

			if (user is null)
				return new ForgotPasswordCommandResponse();

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

			return new ForgotPasswordCommandResponse() { Token = token };
		}
	}
}
