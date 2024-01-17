using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Infrastructure.Repositories.Abstract
{
    public interface IGenericRepository<T>
    {

        public Task<T> GetByIdAsync(int id, bool tracking);

        public Task<IEnumerable<T>> GetAllAsync(bool includeDeleted);
        public Task<IEnumerable<T>> GetLastNAsync(int n, bool includeDeleted);
        public Task<IEnumerable<T>> GetAllPaginatedAsync(int currentpage, int perPage, bool includeDeleted);
        
        public Task CreateAsync(T entity);


        public void Update(T entity);

        public Task SaveChangesAsync();

        public void Delete(T entity);

        public Task<int> GetCountAsync();
    }
}
