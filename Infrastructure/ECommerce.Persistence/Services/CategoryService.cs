using ECommerce.Application.Extentions;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Services
{
	public class CategoryService : ICategoryService
	{

		private readonly IReadCategoryRepository _readCategoryRepository;
		private readonly IWriteCategoryRepository _writeCategoryRepository;

		public CategoryService(IReadCategoryRepository readCategoryRepository, IWriteCategoryRepository writeCategoryRepository)
		{
			_readCategoryRepository = readCategoryRepository;
			_writeCategoryRepository = writeCategoryRepository;
		}

		public async Task AddCategoryAsync(AddCategoryVM categoryVM)
		{
			var newCategory = new Category
			{
				Name = categoryVM.Name,
				Description = categoryVM.Description,
			};
			await _writeCategoryRepository.AddAsync(newCategory);
			await _writeCategoryRepository.SaveChangeAsync();
		}

		public Task<bool> DeleteCategoryAsync(int categoryId)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<GetCategoryVM>> GetAllCategoriesAsync(PaginationVM paginationVM)
		{
			var categories = await _readCategoryRepository.GetAllAsync();
			var categoryForPage = categories.ToList().Paginate(paginationVM.Page);


			var allCategoriesVMs = categoryForPage.Select(p => new GetCategoryVM()
			{
				Name = p.Name,
				Description = p.Description,
			}).ToList();
			return allCategoriesVMs;
		}

		public Task<GetCategoryVM?> GetCategoryByIdAsync(int categoryId)
		{
			throw new NotImplementedException();
		}

		public Task<HttpStatusCode> UpdateCategoryAsync(int id, UpdateCategoryVM updateProductVM)
		{
			throw new NotImplementedException();
		}
	}
}
