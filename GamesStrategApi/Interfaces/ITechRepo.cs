using GamesStrategApi.Models;

namespace GamesStrategApi.Interfaces
{
    public interface ITechRepo:IRepository<Tech>
    {
        /// <summary>
        /// Получить технологии определенного уровня
        /// </summary>
        Task<IEnumerable<Tech>> GetTechsByTierAsync(int tier);

        /// <summary>
        /// Получить доступные стартовые технологии
        /// </summary>
        Task<IEnumerable<Tech>> GetAvailableStartingTechsAsync();

        /// <summary>
        /// Получить полное дерево технологий
        /// </summary>
        Task<IEnumerable<Tech>> GetTechTreeAsync();

        /// <summary>
        /// Получить технологии с их зависимостями
        /// </summary>
        Task<IEnumerable<Tech>> GetTechsWithDependenciesAsync();

        /// <summary>
        /// Получить технологию по ID с информацией о разблокируемых элементах
        /// </summary>
        Task<Tech?> GetTechWithUnlocksAsync(int id);
    }
}
