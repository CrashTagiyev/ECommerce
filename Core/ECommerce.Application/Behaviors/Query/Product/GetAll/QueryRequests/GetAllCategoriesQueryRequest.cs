using ECommerce.Application.Behaviors.Query.Product.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Behaviors.Query.Product.GetAll.QueryRequests
{
    public class GetAllCategoriesQueryRequest : IRequest<GelAllProductsQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
