using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Services
{
	public class UrlHelperService : IUrlHelperService
	{
		private readonly IUrlHelper _urlHelper;

		public UrlHelperService(IUrlHelper urlHelper)
		{
			_urlHelper = urlHelper;
		}

		public string GenerateResetPasswordCallbackUrl(string token, string email)
		{
			return _urlHelper.Action("ResetPasswordConfirm", "Auth", new { token = token, email = email }, "http")!;
		}
	}
}
