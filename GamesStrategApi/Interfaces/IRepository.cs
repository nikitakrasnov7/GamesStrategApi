using System.Linq.Expressions;

namespace GamesStrategApi.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получить все записи
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получить запись по ID
        /// </summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Найти записи по условию
        /// </summary>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Получить первую запись по условию
        /// </summary>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Добавить новую запись
        /// </summary>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Обновить существующую запись
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Удалить запись по ID
        /// </summary>
        Task DeleteAsync(int id);

        /// <summary>
        /// Удалить запись по сущности
        /// </summary>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Проверить существование записи по ID
        /// </summary>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Сохранить изменения в базе данных
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Получить все записи с включением связанных данных
        /// </summary>
        Task<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
    }
}
