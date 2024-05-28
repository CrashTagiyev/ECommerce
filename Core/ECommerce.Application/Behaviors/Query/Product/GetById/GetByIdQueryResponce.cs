using ECommerce.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.Product.GetById
{
	public class GetByIdQueryResponce
	{
        public GetProductVM? ProductVM { get; set; }

        public GetByIdQueryResponce()
        {
				
        }
        public GetByIdQueryResponce(GetProductVM productVM)
		{
			ProductVM = productVM;
		}
	}
}
