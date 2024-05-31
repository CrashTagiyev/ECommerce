using ECommerce.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Repositories.AppUserAbstractRepo
{
	public interface IReadAppUserRepository
	{
		Task<IEnumerable<AppUser>> GetAllUsersAsync();
	}
}
