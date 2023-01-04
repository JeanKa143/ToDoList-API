using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Data;

namespace ToDoLIst_DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext AppDbContext { get; set; }

        public Repository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        public IQueryable<T> FindAll() => AppDbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            AppDbContext.Set<T>().Where(expression);

        public void Create(T entity) => AppDbContext.Set<T>().Add(entity);

        public void Update(T entity) => AppDbContext.Set<T>().Update(entity);

        public void Delete(T entity) => AppDbContext.Set<T>().Remove(entity);

        public void UpdateRange(IEnumerable<T> entities) => AppDbContext.Set<T>().UpdateRange(entities);
    }
}
