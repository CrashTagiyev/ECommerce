using ECommerce.Domain.Entities.Concretes;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Services
{
	public  class EmailService:IEmailService
	{

		private readonly IConfiguration _config;
		private readonly UserManager<AppUser> _userManager;
		public EmailService(IConfiguration config, UserManager<AppUser> userManager)
		{
			_config = config;
			_userManager = userManager;
		}
		public Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML)
		{
			string MailServer = _config["EmailSettings:MailServer"]!;
			string FromEmail = _config["EmailSettings:FromEmail"]!;
			string Password = _config["EmailSettings:Password"]!;
			int Port = int.Parse(_config["EmailSettings:MailPort"]!);
			var client = new SmtpClient(MailServer, Port)
			{
				Credentials = new NetworkCredential(FromEmail, Password),
				EnableSsl = true,
			};
			MailMessage mailMessage = new MailMessage(FromEmail, toEmail, subject, body)
			{
				IsBodyHtml = isBodyHTML
			};
			return client.SendMailAsync(mailMessage);
		}
		public async Task SendConfirmationEmail(string? email, AppUser? user)
		{
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user!);
			var confirmationLink = $"http://localhost:5046/api/auth/ConfirmEmail?UserId={user!.Id}&Token={token}";
			await SendEmailAsync(email!, "Confirm Your Email", $"Please confirm your account by <a href='{confirmationLink}'>{confirmationLink}</a>;.", true);
		}


	}
}
