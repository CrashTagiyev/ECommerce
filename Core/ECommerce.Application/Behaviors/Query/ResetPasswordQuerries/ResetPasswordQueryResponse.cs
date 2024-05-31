using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.ResetPasswordQuerries
{
	public class ResetPasswordQueryResponse
	{
		public string? Message { get; set; }
        public bool IsPasswordChanged { get; set; }
    }
}
