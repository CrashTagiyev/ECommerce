using ECommerce.Domain.Entities.Concretes;

namespace ECommerce.Domain.DTOs;

public class AppUserDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }


}
