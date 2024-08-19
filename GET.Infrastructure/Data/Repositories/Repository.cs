using GET.Domain.Interfaces;
using GET.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GET.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public async Task<IReadOnlyList<T>> ListAllAsync() => await _context.Set<T>().ToListAsync();
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
                 => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
          => await ApplySpecification(spec).ToListAsync();

        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
          => await ApplySpecification(spec).FirstOrDefaultAsync();
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

}
