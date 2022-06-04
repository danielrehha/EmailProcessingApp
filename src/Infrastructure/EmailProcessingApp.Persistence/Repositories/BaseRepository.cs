using EmailProcessingApp.Application.Contract.Persistence;
using EmailProcessingApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailProcessingApp.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreationDate = DateTime.Now;

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IReadOnlyList<T>> QueryAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<T> UpdateAsync(Guid id, T entity)
        {
            var result = await _context.Set<T>().FindAsync(id);

            _context.Entry(result).State = EntityState.Detached;
            result = entity;
            _context.Set<T>().Update(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<bool> DoesExistAsync(Guid id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
