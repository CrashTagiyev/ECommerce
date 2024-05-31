using ECommerce.Application.Repositories.AppUserAbstractRepo;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories.AppUserRepos
{
	public class ReadAppUserRepository:IReadAppUserRepository
	{
		private readonly ECommerceDbContext _context;

		public ReadAppUserRepository(ECommerceDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
		{
			return  _context.Users;
		}
	}
}
