using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public abstract class GenericRepositoryBase<T> : IGenericRepository<T> where T : class
    {
        protected readonly dbContext _context;
        protected GenericRepositoryBase(dbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression).AsNoTracking();
        public async Task<T?> FindByIdAsync(Guid id) => await _context.Set<T>().FindAsync(id);

        public Task<List<T>> GetAllAsync() => _context.Set<T>().ToListAsync();
        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
