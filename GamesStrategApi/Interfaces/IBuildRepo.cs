using GamesStrategApi.Models;

namespace GamesStrategApi.Interfaces
{
    public interface IBuildRepo : IRepository<Building>
    {
        /// <summary>
        /// Получить здания определенного типа
        /// </summary>
        Task<IEnumerable<Building>> GetBuildingsByTypeAsync(string buildingType);

        /// <summary>
        /// Получить экономические здания
        /// </summary>
        Task<IEnumerable<Building>> GetEconomicBuildingsAsync();

        /// <summary>
        /// Получить здания, доступные без исследования технологий
        /// </summary>
        Task<IEnumerable<Building>> GetAvailableWithoutTechAsync();

        /// <summary>
        /// Получить дорогие здания (стоимость выше указанной)
        /// </summary>
        Task<IEnumerable<Building>> GetExpensiveBuildingsAsync(int minCost);

        /// <summary>
        /// Получить здания с информацией о требуемых технологиях
        /// </summary>
        Task<IEnumerable<Building>> GetBuildingsWithTechAsync();

        /// <summary>
        /// Получить здание по ID с информацией о требуемой технологии
        /// </summary>
        Task<Building?> GetBuildingWithTechAsync(int id);
    }
}
