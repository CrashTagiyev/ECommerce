using ECommerce.Application.Behaviors.Query.Product.GetAll.QueryRequests;
using ECommerce.Application.Behaviors.Query.Product.GetAll;
using ECommerce.Application.Services;
using ECommerce.Domain.ViewModels;
using MediatR;
using ECommerce.Domain.Entities.Concretes;

namespace ECommerce.Application.Behaviors.Query.Product.GetById
{
	public class GetByIdQueryHandler : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponce>
	{
		private readonly IProductService _productService;

		public GetByIdQueryHandler(IProductService productService, ICategoryService categoryService)
		{
			_productService = productService;
		}

		public async Task<GetByIdQueryResponce> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var productVM = await _productService.GetProductByIdAsync(request.Id);
			if (productVM == null)
				return new GetByIdQueryResponce()!;


			return new GetByIdQueryResponce(productVM);
		}
	}

}
