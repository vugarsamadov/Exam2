using Exam2.Business.Models;
using Exam2.Business.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Business.Services.Abstract
{
    public interface IAboutItemService
    {
        public Task<GenericPaginatedEntity<AboutItemVM>> GetAllPaginatedAsync(int currentpage, int perPage);

        public Task<IEnumerable<AboutItemVM>> GetLastNAsync(int n);

        public Task<AboutItemVM> GetByIdAsync(int id);

        public Task UpdateAsync(int id, AboutItemUpdateVM model);
        
        public Task CreateAsync(AboutItemCreateVM model);

        public Task SoftDeleteAsync(int id);
        public Task RevokeDeleteAsync(int id);

    }
}
