using Exam2.Core.Entities;
using Exam2.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private ApplicationDbContext _dbContext { get; }
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool includeDeleted)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (!includeDeleted)
                query = query.Where(e => e.IsDeleted != true);

            var entities = await query.ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id, bool tracking)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            var entity = await query.FirstOrDefaultAsync(e=> e.Id == id);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        

        public async Task<IEnumerable<T>> GetLastNAsync(int n, bool includeDeleted)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (!includeDeleted)
                query = query.Where(e => e.IsDeleted != true);
            query.OrderByDescending(e => e.CreatedAt).Take(n);

            var entities = await query.ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetAllPaginatedAsync(int currentpage, int perPage, bool includeDeleted)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (!includeDeleted)
                query = query.Where(e => e.IsDeleted != true);

            var entities = await query.Skip((currentpage - 1) * perPage).Take(perPage).ToListAsync();
            return entities;
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }
    }
}
