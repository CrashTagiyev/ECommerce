using ECommerce.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Command.ForgotPasswordCommands
{
	public class ForgotPasswordCommandRequest:IRequest<ForgotPasswordCommandResponse>
	{
		[EmailAddress]
		[Required]
        public string? Email { get; set; }
    }
}
