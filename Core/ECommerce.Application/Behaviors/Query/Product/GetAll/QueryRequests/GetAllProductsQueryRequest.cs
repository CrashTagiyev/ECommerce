using ECommerce.Application.Behaviors.Query.Product.GetAll;
using MediatR;
namespace ECommerce.Application.Behaviors.Query.Product.GetAll.QueryRequests;

public class GetAllProductsQueryRequest : IRequest<GelAllProductsQueryResponse>
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 10;
}
