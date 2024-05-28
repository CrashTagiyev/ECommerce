using ECommerce.Domain.DTOs;

namespace ECommerce.Application.Services;

public interface IAuthService
{
	public Task LogIn(LoginDTO loginDTO);
	public Task Registration(AppUserDTO appUserDTO);
}
