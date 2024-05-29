using ECommerce.Application.Behaviors.Command.UserCommands;
using ECommerce.Application.Behaviors.Query.AppUser.LogIn;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.DTOs;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly UserManager<AppUser> _userManager;
	public AuthController(IMediator mediator, UserManager<AppUser> userManager)
	{
		_mediator = mediator;
		_userManager = userManager;
	}

	[HttpPost("LogIn")]
	public async Task<IActionResult> Login([FromBody] LogInQueryRequest request)
	{
		var responce = await _mediator.Send(request);

		if (responce.EnailConfirmMessage is not null)
			return BadRequest(responce.EnailConfirmMessage);


		if (responce.PasswordWrongMessage is not null)
			return BadRequest(responce.PasswordWrongMessage);

		if (responce.Token is null)
			return BadRequest("Invalid username or password");



		return Ok(new { token = responce.Token });
	}


	// Add User Method
	[HttpPost("AddUser")]
	public async Task<IActionResult> RegisterUser([FromBody] UserRegisterCommandRequest request)
	{
		var responce = await _mediator.Send(request);

		if (responce.IsCreated is false)
			return BadRequest(responce.ConfirmMessage);
		
		return Ok(responce.ConfirmMessage);

	}

	[Authorize(Roles = "Admin")]
	[HttpGet("[action]")]
	public IActionResult SomeMethod()
	{
		var identity = HttpContext.User.Identity as ClaimsIdentity;
		var claims = identity.Claims;

		var user = new AppUser()
		{
			UserName = claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value,
			Email = claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value,
			Role = claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value
		};

		return Ok(user);
	}

	[HttpGet("ConfirmEmail")]
	public async Task<IActionResult> ConfirmEmail( string userId, string token)
	{
		var user = await _userManager.FindByIdAsync(userId);

		if (userId == null || token == null)
			return BadRequest("Link expired");

		else if (user == null)
			return BadRequest("User not Found");

		else
		{
			token = token.Replace(" ", "+");
			var result = await _userManager.ConfirmEmailAsync(user, token);

			if (result.Succeeded)
				return Ok("Thank you for confirming your email");

			else
				return BadRequest("Email not confirmed");
		}
	}

}
