using ECommerce.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
	public interface ICategoryService
	{
		Task<ICollection<GetCategoryVM>> GetAllCategoriesAsync(PaginationVM paginationVM);
		Task<GetCategoryVM?> GetCategoryByIdAsync(int categoryId);
		Task AddCategoryAsync(AddCategoryVM categoryVM);
		Task<HttpStatusCode> UpdateCategoryAsync(int id, UpdateCategoryVM updateProductVM);
		Task<bool> DeleteCategoryAsync(int categoryId);
	}
}
