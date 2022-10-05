using EmployeeManagement.Contracts;
using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DataAccess _context;

        public BaseRepository(DataAccess context)
        {
            this._context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            var newEntity = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entityToDelete = await _context.Set<T>().FindAsync(id);
            if (entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
                await _context.SaveChangesAsync();
                return entityToDelete;
            }
            return null!;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result != null) return result;
            return null!;
        }

        public async Task<T> UpdateByAsync(T entity)
        {
            var entityToDelete = _context.Update(entity);
            await _context.SaveChangesAsync();
            return entityToDelete.Entity;
        }

    }

}
