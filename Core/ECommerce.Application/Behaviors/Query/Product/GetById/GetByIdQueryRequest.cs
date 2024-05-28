using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.Product.GetById
{
	public class GetByIdQueryRequest:IRequest<GetByIdQueryResponce>
	{
        public int Id { get; set; }
    }
}
