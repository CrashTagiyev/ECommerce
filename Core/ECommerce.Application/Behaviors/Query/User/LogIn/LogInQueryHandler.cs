using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.AppUser.LogIn
{
	public class LogInQueryHandler : IRequestHandler<LogInQueryRequest, LogInQueryResponce>
	{
		private readonly IReadAppUserRepository _readAppUserRepository;
		private readonly ITokenService _tokenService;


		public LogInQueryHandler(IReadAppUserRepository readAppUserRepository, ITokenService tokenService)
		{
			_readAppUserRepository = readAppUserRepository;
			_tokenService = tokenService;
		}

		public async Task<LogInQueryResponce> Handle(LogInQueryRequest request, CancellationToken cancellationToken)
		{
			var user = await _readAppUserRepository.GetUserByUserNameAndPassword(request.UserName!, request.Password!);

			if (user is null)
				return new LogInQueryResponce();

			var token = _tokenService.CreateToken(user);
				return new LogInQueryResponce { Token=token};
		}
	}
}
