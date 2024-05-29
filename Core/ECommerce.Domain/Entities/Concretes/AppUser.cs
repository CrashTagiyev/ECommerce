using ECommerce.Domain.Entities.Abstracts;
using ECommerce.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities.Concretes;

public class AppUser :IdentityUser 
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }

 
    // Navigation Property
    public  virtual ICollection<Order> Orders { get; set; }

}
