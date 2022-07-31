using Microsoft.EntityFrameworkCore;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Data;

namespace ToDoLIst_DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            if (entity is null)
            {
                throw new NullReferenceException();
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var numbOfItemsToSkip = (pageNumber - 1) * pageSize;

            var items = await _context.Set<T>()
                .Skip(numbOfItemsToSkip)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return items;
        }

        public Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
