using ECommerce.Domain.ViewModels;
using System.Net;

namespace ECommerce.Application.Behaviors.Query.Product.GetAll;

public class GelAllProductsQueryResponse
{
    public ICollection<GetProductVM> Products { get; set; }

    public GelAllProductsQueryResponse(ICollection<GetProductVM> products)
    {
        Products = products;
    }


}
