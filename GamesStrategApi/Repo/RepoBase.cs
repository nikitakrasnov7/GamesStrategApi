using GamesStrategApi.Interfaces;
using GamesStrategApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GamesStrategApi.Repo
{
    public abstract class RepoBase<T> : IRepository<T> where T : class
    {
        protected readonly StrategContext _context;
        protected readonly DbSet<T> _dbSet;

        protected RepoBase(StrategContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Получить все записи
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Получить запись по ID
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Найти записи по условию
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Получить первую запись по условию
        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        // Добавить новую запись
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        // Обновить существующую запись
        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
        }

        // Удалить запись по ID
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        // Удалить запись по сущности
        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        // Проверить существование записи по ID
        public virtual async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        // Сохранить изменения в БД
        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Получить все записи с включением связанных данных
        public virtual async Task<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        // Получить запрос к БД
        protected IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}

