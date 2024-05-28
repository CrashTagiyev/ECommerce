using ECommerce.Application.Behaviors.Query.AppUser.LogIn;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.DTOs;
using ECommerce.Domain.Entities.Concretes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IReadAppUserRepository _readAppUserRepository;
    private readonly IWriteAppUserRepository _writeAppUserRepository;
    private readonly ITokenService _tokenService;
	private readonly IMediator _mediator;

	public AuthController(IReadAppUserRepository readAppUserRepository, ITokenService tokenService, IWriteAppUserRepository writeAppUserRepository, IMediator mediator)
	{
		_readAppUserRepository = readAppUserRepository;
		_tokenService = tokenService;
		_writeAppUserRepository = writeAppUserRepository;
		_mediator = mediator;
	}

	[HttpPost("LogIn")]
    public async Task<IActionResult> Login([FromBody] LogInQueryRequest request)
    {
        var responce = await _mediator.Send(request);
        if (responce.Token is null)
            return BadRequest("Invalid username or password");

        return Ok(new { token = responce.Token });
    }


    // Add User Method
    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser([FromBody] AppUserDTO appUserDTO)
    {
        var user = await _readAppUserRepository.GetUserByUserName(appUserDTO.UserName);
        if (user is not null)
            return BadRequest("User already exists");

        var newUser = new AppUser()
        {
            UserName = appUserDTO.UserName,
            Email = appUserDTO.Email,
            Password = appUserDTO.Password,
            Role = appUserDTO.Role
        };

        await _writeAppUserRepository.AddAsync(newUser);
        await _writeAppUserRepository.SaveChangeAsync();
        return Ok();
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
}
