using Amazon.core.Entities;
using Amazon.core.Repositories;
using Amazon.core.Specifications;
using Amazon.repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await applySpecification(spec).ToListAsync();
        }
        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await applySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await applySpecification(spec).CountAsync();
        }
        private IQueryable<T> applySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(),spec);
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }
    }
}
