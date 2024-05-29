using ECommerce.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ECommerce.Application.Services;

namespace ECommerce.Infrastructure.Services
{
	public static class EmailService
	{


		private static string GenerateUserEmailConfirmationToken(AppUser user)
		{
			// For demonstration, we'll create a simple token using user's email and a timestamp
			// In a real-world scenario, use a more secure approach like JWT tokens
			string token = $"{user.Email}-{DateTime.UtcNow.Ticks}";
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
				StringBuilder builder = new StringBuilder();
				foreach (byte b in bytes)
				{
					builder.Append(b.ToString("x2"));
				}
				return builder.ToString();
			}
		}
		public static void SendUserConfirmEmail(AppUser user)
		{
			if (user == null || string.IsNullOrEmpty(user.Email))
			{
				throw new ArgumentException("User or user's email is null or empty.");
			}

			string token = GenerateUserEmailConfirmationToken(user);
			string confirmationLink = $"http://localhost:5046/confirmemail?token={token}";

			string subject = "Confirm your email address";
			string body = $"Hi {user.FirstName},\n\nPlease confirm your email address by clicking the following link:\n{confirmationLink}\n\nThank you!";

			SendEmail(user.Email, subject, body);
		}

		private static void SendEmail(string toEmail, string subject, string body)
		{
			string fromEmail = "your-email@example.com";
			string fromPassword = "your-email-password";

			MailMessage message = new MailMessage();
			message.From = new MailAddress(fromEmail);
			message.To.Add(toEmail);
			message.Subject = subject;
			message.Body = body;

			SmtpClient smtpClient = new SmtpClient("smtp.example.com")
			{
				Port = 587,
				Credentials = new NetworkCredential(fromEmail, fromPassword),
				EnableSsl = true,
			};

			try
			{
				smtpClient.Send(message);
				Console.WriteLine("Email sent successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to send email: {ex.Message}");
			}
		}

	}
}
