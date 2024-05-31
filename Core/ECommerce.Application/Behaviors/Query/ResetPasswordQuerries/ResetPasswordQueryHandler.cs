using ECommerce.Application.Services;
using ECommerce.Domain.Entities.Concretes;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.ResetPasswordQuerries
{
	public class ResetPasswordQueryHandler : IRequestHandler<ResetPasswordQueryRequest, ResetPasswordQueryResponse>
	{

		private readonly IAppUserService _appUserService;

		public ResetPasswordQueryHandler(UserManager<Domain.Entities.Concretes.AppUser> userManager, IAppUserService appUserService)
		{
			_appUserService = appUserService;
		}

		public async Task<ResetPasswordQueryResponse> Handle(ResetPasswordQueryRequest request, CancellationToken cancellationToken)
		{

			var searchedUser = await _appUserService.FindUserByTokenAsync(request.Token!);

			if (request.Token is null || request.NewPassword is null)
				return new ResetPasswordQueryResponse()
				{
					Message = "Invalid Token or password",
					IsPasswordChanged = false,
				};

			if (searchedUser is null)
				return new ResetPasswordQueryResponse()
				{
					Message = "Invalid Token.User did not found",
					IsPasswordChanged = false,
				};

			var result =await _appUserService.ResetPasswordAsync(searchedUser, request.Token,request.NewPassword);
			
			if (result.Succeeded)
				return new ResetPasswordQueryResponse()
				{
					Message = "Password successfully changed",
					IsPasswordChanged = true,
				};

			return new ResetPasswordQueryResponse()
			{
				Message = "Invalid Token.",
				IsPasswordChanged = false,
			};
		}
	}
}
