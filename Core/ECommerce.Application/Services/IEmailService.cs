using ECommerce.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
	public interface IEmailService
	{
		Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML);
		Task SendConfirmationEmail(string? email, AppUser? user);
		Task SendResetPasswordEmailAsync(string? email, AppUser? user);
	}

}
