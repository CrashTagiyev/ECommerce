using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.ResetPasswordQuerries
{
	public class ResetPasswordQueryRequest:IRequest<ResetPasswordQueryResponse>
	{
        public string? Token { get; set; }
        public string? NewPassword { get; set; }
    }
}
